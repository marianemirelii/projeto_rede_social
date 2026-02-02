import { configureStore } from "@reduxjs/toolkit";
import authReducer from "./authSlice";
import userReducer from "./userSlice";
import postsReducer from "./postsSlice";
import commentsReducer from "./commentsSlice";
import profileReducer from "./profileSlice";
import friendsReducer from "./friendsSlice";

export const store = configureStore({
  reducer: {
    auth: authReducer,
    user: userReducer,
    posts: postsReducer,
    comments: commentsReducer,
    profile: profileReducer,
    friends: friendsReducer
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;