import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../../store";
import { getLoggedUser } from "../../store/userSlice";
import { logout } from "../../store/authSlice";
import { PostList } from "../../components/PostList/PostList";

import { Header } from "../../components/Header/Header";
import "./Home.css";
import { loadPosts } from "../../store/postsSlice";

type Section = "feed" | "profile" | "search" | "notifications";

export function Home() {
  const dispatch = useDispatch<AppDispatch>();

  const user = useSelector((state: RootState) => state.user.data);
  const userLoading = useSelector(
    (state: RootState) => state.user.loading
  );

  const [activeSection, setActiveSection] =
    useState<Section>("feed");

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
        {activeSection === "feed" && <PostList />}

        {activeSection === "profile" && (
          <div className="placeholder">
            Perfil do usuário
          </div>
        )}

        {activeSection === "search" && (
          <div className="placeholder">
            Buscar usuários
          </div>
        )}

        {activeSection === "notifications" && (
          <div className="placeholder">
            Notificações / solicitações
          </div>
        )}
      </main>
    </div>
  );
}
