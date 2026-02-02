import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { fetchProfile, updateProfile } from "../services/userService";

interface ProfileState {
  data: any;
  loading: boolean;
  error: string | null;
}

const initialState: ProfileState = {
  data: null,
  loading: false,
  error: null,
};

export const loadProfile = createAsyncThunk(
  "profile/load",
  async (_, { rejectWithValue }) => {
    try {
      return await fetchProfile();
    } catch {
      return rejectWithValue("Erro ao carregar perfil");
    }
  }
);

export const editProfile = createAsyncThunk(
  "profile/edit",
  async (
    data: { nickname?: string | null; image?: string | null },
    { rejectWithValue }
  ) => {
    try {
      return await updateProfile(data);
    } catch {
      return rejectWithValue("Erro ao editar perfil");
    }
  }
);

const profileSlice = createSlice({
  name: "profile",
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder
      .addCase(loadProfile.pending, state => {
        state.loading = true;
      })
      .addCase(loadProfile.fulfilled, (state, action) => {
        state.loading = false;
        state.data = action.payload;
      })
      .addCase(editProfile.fulfilled, (state, action) => {
        state.data = {
          ...state.data,
          ...action.payload,
        };
      });
  },
});

export default profileSlice.reducer;
