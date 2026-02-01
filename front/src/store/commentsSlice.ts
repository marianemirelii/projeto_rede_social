import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { fetchComments, createComment } from "../services/commentService";
import type { Comment } from "../types/Comment";

interface CommentsState {
  comments: Comment[];
  loading: boolean;
  error: string | null;
}

const initialState: CommentsState = {
  comments: [],
  loading: false,
  error: null,
};

export const loadComments = createAsyncThunk<
  Comment[],
  number,
  { rejectValue: string }
>("comments/load", async (postId, { rejectWithValue }) => {
  try {
    return await fetchComments(postId);
  } catch {
    return rejectWithValue("Erro ao carregar comentários");
  }
});

export const addComment = createAsyncThunk<
  void,
  { postId: number; content: string },
  { rejectValue: string }
>(
  "comments/create",
  async ({ postId, content }, { dispatch, rejectWithValue }) => {
    try {
      await createComment(postId, content);

      dispatch(loadComments(postId));
    } catch {
      return rejectWithValue("Erro ao comentar");
    }
  }
);


const commentsSlice = createSlice({
  name: "comments",
  initialState,
  reducers: {
    clearComments(state) {
      state.comments = [];
    },
  },
  extraReducers: builder => {
    builder
      .addCase(loadComments.pending, state => {
        state.loading = true;
        state.error = null;
      })
      .addCase(loadComments.fulfilled, (state, action) => {
        state.loading = false;
        state.comments = action.payload;
      })
      .addCase(loadComments.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload ?? "Erro inesperado";
      });
  },
});


export const { clearComments } = commentsSlice.actions;
export default commentsSlice.reducer;
