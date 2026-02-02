import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { fetchFriends } from "../services/friendService";

interface FriendsState {
  list: any[];
  loading: boolean;
}

const initialState: FriendsState = {
  list: [],
  loading: false,
};

export const loadFriends = createAsyncThunk(
  "friends/load",
  async (_, { rejectWithValue }) => {
    try {
      return await fetchFriends();
    } catch {
      return rejectWithValue("Erro ao carregar amigos");
    }
  }
);

const friendsSlice = createSlice({
  name: "friends",
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder
      .addCase(loadFriends.pending, state => {
        state.loading = true;
      })
      .addCase(loadFriends.fulfilled, (state, action) => {
        state.loading = false;
        state.list = action.payload;
      });
  },
});

export default friendsSlice.reducer;
