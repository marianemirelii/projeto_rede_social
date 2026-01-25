import { useState } from "react";
import { Header } from "../../components/Header/Header";
import type { User } from "../../types/User";
import "./Home.css";

type Section = "feed" | "profile" | "search" | "notifications";

const userMock: User = {
  id: 5,
  name: "Luiz Alberto",
  email: "luiz@gmail.com",
  birthDate: "1982-01-15",
  cep: "58808583",
  image: null,
  nickName: "Luiz.A",
  postsCount: 0,
  friendsCount: 0,
};

export function Home() {
  const [activeSection, setActiveSection] =
    useState<Section>("feed");

  function handleLogout() {
    // depois: dispatch(logout())
    localStorage.removeItem("token");
    window.location.href = "/login";
  }

  return (
    <div className="home-container">
      <Header
        user={userMock}
        activeSection={activeSection}
        onChangeSection={setActiveSection}
        onLogout={handleLogout}
      />

      <main className="home-content">
        {activeSection === "feed" && (
          <div className="placeholder">
            Feed (PostList vem aqui)
          </div>
        )}

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
