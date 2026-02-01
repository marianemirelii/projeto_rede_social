import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import type { Post } from "../types/Post";
import { fetchPosts, createPost } from "../services/postService";
import { likePostApi, unlikePostApi } from "../services/likeService";

interface PostsState {
  posts: Post[];
  loading: boolean;
  error: string | null;
}

const initialState: PostsState = {
  posts: [],
  loading: false,
  error: null,
};

export const loadPosts = createAsyncThunk<
  Post[],
  void,
  { rejectValue: string }
>("posts/load", async (_, { rejectWithValue }) => {
  try {
    return await fetchPosts();
  } catch {
    return rejectWithValue("Erro ao carregar feed");
  }
});

export const addPost = createAsyncThunk<
  Post,
  { content: string; isPublic: boolean; image: string | null },
  { rejectValue: string }
>("posts/create", async (data, { rejectWithValue }) => {
  try {
    return await createPost(data);
  } catch {
    return rejectWithValue("Erro ao criar post");
  }
});

const postsSlice = createSlice({
  name: "posts",
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder
      .addCase(loadPosts.pending, state => {
        state.loading = true;
        state.error = null;
      })
      .addCase(loadPosts.fulfilled, (state, action) => {
        state.loading = false;
        state.posts = action.payload;
      })
      .addCase(loadPosts.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload ?? "Erro inesperado";
      })
      .addCase(addPost.fulfilled, (state, action) => {
        state.posts.unshift(action.payload);
      })
      .addCase(toggleLike.fulfilled, (state, action) => {
        const { postId, liked } = action.payload;

        const post = state.posts.find(p => p.id === postId);
        if (!post) return;

        post.likedByMe = liked;
        post.likes = liked ? post.likes + 1 : post.likes - 1;
      });
  },
});


export const toggleLike = createAsyncThunk<
  { postId: number; liked: boolean },
  { postId: number; liked: boolean },
  { rejectValue: string }
>("posts/toggleLike", async ({ postId, liked }, { rejectWithValue }) => {
  try {
    if (liked) {
      await unlikePostApi(postId);
      return { postId, liked: false };
    } else {
      await likePostApi(postId);
      return { postId, liked: true };
    }
  } catch {
    return rejectWithValue("Erro ao curtir post");
  }
});


export default postsSlice.reducer;
