import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import type { Post } from "../types/Post";
import { fetchPosts, createPost } from "../services/postService";

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
  reducers: {
    likePost(state, action) {
      const post = state.posts.find(p => p.id === action.payload);
      if (post) post.likes += 1;
    },
  },
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
    });
  },
});

export const { likePost } = postsSlice.actions;
export default postsSlice.reducer;
