import type { User } from "../../types/User";
import "./Header.css";

interface HeaderProps {
  user: User;
}

export function Header({ user }: HeaderProps) {
  return (
    <header className="app-header">
      <div className="header-logo">Social</div>

      <nav className="header-nav">
        <button className="nav-item active">🏠 Feed</button>
        <button className="nav-item">👤 Perfil</button>
      </nav>

      <div className="header-user">
        {user.image ? (
          <img src={user.image} alt={user.name} />
        ) : (
          <div className="user-avatar">
            {user.name.charAt(0)}
          </div>
        )}
        <span>{user.nickName}</span>
      </div>
    </header>
  );
}
