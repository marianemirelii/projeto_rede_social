import "./Header.css";
import type { User } from "../../types/User";

type Section = "feed" | "profile" | "search" | "notifications";

interface HeaderProps {
  user: User;
  activeSection: Section;
  onChangeSection: (section: Section) => void;
  onLogout: () => void;
}

export function Header({
  user,
  activeSection,
  onChangeSection,
  onLogout,
}: HeaderProps) {
  return (
    <header className="app-header">
      <div className="header-logo">DevHub</div>

      <nav className="header-nav">
        <button
          className={`nav-item ${activeSection === "feed" ? "active" : ""}`}
          onClick={() => onChangeSection("feed")}
        >
          🏠 Feed
        </button>

        <button
          className={`nav-item ${activeSection === "profile" ? "active" : ""}`}
          onClick={() => onChangeSection("profile")}
        >
          👤 Perfil
        </button>

        <button
          className={`nav-item ${activeSection === "search" ? "active" : ""}`}
          onClick={() => onChangeSection("search")}
        >
          🔍 Buscar
        </button>

        <button
          className={`nav-item ${
            activeSection === "notifications" ? "active" : ""
          }`}
          onClick={() => onChangeSection("notifications")}
        >
          🔔 Notificações
        </button>
      </nav>

      <div className="header-user">
        {user.image ? (
          <img src={user.image} alt={user.name} />
        ) : (
          <div className="user-avatar">
            {user.name.charAt(0)}
          </div>
        )}

        <span className="user-nick">@{user.nickName}</span>

        <button className="logout-btn" onClick={onLogout}>
          Sair
        </button>
      </div>
    </header>
  );
}
