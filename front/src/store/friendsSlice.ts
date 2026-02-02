import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import {
  fetchFriends,
  fetchFriendRequests,
  respondFriendRequest,
} from "../services/friendService";

interface FriendsState {
  list: any[];
  requests: any[];
  loading: boolean;
  requestsLoading: boolean;
}

const initialState: FriendsState = {
  list: [],
  requests: [],
  loading: false,
  requestsLoading: false,
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


export const loadFriendRequests = createAsyncThunk(
  "friends/loadRequests",
  async (_, { rejectWithValue }) => {
    try {
      return await fetchFriendRequests();
    } catch {
      return rejectWithValue("Erro ao carregar solicitações");
    }
  }
);


export const answerFriendRequest = createAsyncThunk(
  "friends/answerRequest",
  async (
    { friendId, status }: { friendId: number; status: number },
    { rejectWithValue }
  ) => {
    try {
      await respondFriendRequest(friendId, status);
      return { friendId };
    } catch {
      return rejectWithValue("Erro ao responder solicitação");
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
      })

      
      .addCase(loadFriendRequests.pending, state => {
        state.requestsLoading = true;
      })
      .addCase(loadFriendRequests.fulfilled, (state, action) => {
        state.requestsLoading = false;
        state.requests = action.payload;
      })

     
      .addCase(answerFriendRequest.fulfilled, (state, action) => {
        state.requests = state.requests.filter(
          r => r.friend.idFriend !== action.payload.friendId
        );
      });
  },
});

export default friendsSlice.reducer;
