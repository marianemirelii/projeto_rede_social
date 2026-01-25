import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import type { User } from "../types/User";
import { fetchLoggedUser } from "../services/userService";

interface UserState {
  data: User | null;
  loading: boolean;
  error: string | null;
}

const initialState: UserState = {
  data: null,
  loading: false,
  error: null,
};

export const getLoggedUser = createAsyncThunk<
  User,
  void,
  { state: any; rejectValue: string }
>("user/getLoggedUser", async (_, { getState, rejectWithValue }) => {
  try {
    const token = getState().auth.token;
    if (!token) throw new Error("Token não encontrado");

    return await fetchLoggedUser(token);
  } catch {
    return rejectWithValue("Erro ao carregar usuário");
  }
});

const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    clearUser(state) {
      state.data = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(getLoggedUser.pending, (state) => {
        state.loading = true;
      })
      .addCase(getLoggedUser.fulfilled, (state, action) => {
        state.loading = false;
        state.data = action.payload;
      })
      .addCase(getLoggedUser.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload ?? "Erro desconhecido";
      });
  },
});

export const { clearUser } = userSlice.actions;
export default userSlice.reducer;
