import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../../store";
import { getLoggedUser } from "../../store/userSlice";
import { logout } from "../../store/authSlice";
import { PostList } from "../../components/PostList/PostList";
import { CreatePost } from "../../components/CreatePost/CreatePost";
import { Header } from "../../components/Header/Header";
import "./Home.css";
import { loadPosts } from "../../store/postsSlice";
import { PostComments } from "../../components/PostComments/PostComments";
import { Profile } from "../../components/Profile/Profile";
import { FriendRequests } from "../../components/FriendRequests/FriendRequests";

type Section = "feed" | "profile" | "search" | "notifications";

export function Home() {
  const dispatch = useDispatch<AppDispatch>();

  const user = useSelector((state: RootState) => state.user.data);
  const userLoading = useSelector(
    (state: RootState) => state.user.loading
  );

  const [activeSection, setActiveSection] =
    useState<Section>("feed");

  const [selectedPostId, setSelectedPostId] =
    useState<number | null>(null);

  useEffect(() => {
    if (!user) {
      dispatch(getLoggedUser());
    } else {
      dispatch(loadPosts());
    }
  }, [dispatch, user]);

  function handleLogout() {
    dispatch(logout());
    localStorage.removeItem("token");
    window.location.href = "/login";
  }

  if (userLoading || !user) {
    return <p style={{ padding: 16 }}>Carregando...</p>;
  }

  return (
    <div className="home-container">
      <Header
        user={user}
        activeSection={activeSection}
        onChangeSection={setActiveSection}
        onLogout={handleLogout}
      />

      <main className="home-content">
        {activeSection === "feed" && (
          <>
            {selectedPostId === null ? (
              <>
                <CreatePost />
                <PostList
                  onOpenComments={(postId) =>
                    setSelectedPostId(postId)
                  }
                />
              </>
            ) : (
              <>
                <button
                  className="btn-back"
                  onClick={() => setSelectedPostId(null)}
                >
                  ← Voltar para o feed
                </button>

                <PostComments postId={selectedPostId} />
              </>
            )}
          </>
        )}

        {activeSection === "profile" && <Profile />}

        {activeSection === "search" && (
          <div className="placeholder">Buscar usuários</div>
        )}

        {activeSection === "notifications" && <FriendRequests />}
      </main>
    </div>
  );
}
