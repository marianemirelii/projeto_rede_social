import type { User } from "../../types/User";
import "./ProfileCard.css";

interface ProfileCardProps {
  user: User;
}

export function ProfileCard({ user }: ProfileCardProps) {
  return (
    <aside className="profile-card">
      <div className="profile-avatar">
        {user.image ? (
          <img src={user.image} alt={user.name} />
        ) : (
          <div className="profile-avatar-placeholder">
            {user.name.charAt(0)}
          </div>
        )}
      </div>

      <div className="profile-info">
        <strong className="profile-name">{user.name}</strong>
        <span className="profile-nickname">@{user.nickName}</span>

        <div className="profile-stats">
          <div>
            <strong>{user.postsCount}</strong>
            <span>Posts</span>
          </div>
          <div>
            <strong>{user.friendsCount}</strong>
            <span>Amigos</span>
          </div>
        </div>
      </div>
    </aside>
  );
}
