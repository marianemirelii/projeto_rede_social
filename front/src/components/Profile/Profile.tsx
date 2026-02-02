import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../../store";
import { loadProfile, editProfile } from "../../store/profileSlice";
import { loadFriends } from "../../store/friendsSlice";
import "./Profile.css";

export function Profile() {
  const dispatch = useDispatch<AppDispatch>();

  const { data: user, loading } = useSelector(
    (state: RootState) => state.profile
  );

  const friends = useSelector(
    (state: RootState) => state.friends.list
  );

  const [editing, setEditing] = useState(false);
  const [showFriends, setShowFriends] = useState(false);

  const [nickname, setNickname] = useState("");
  const [image, setImage] = useState("");

  useEffect(() => {
    dispatch(loadProfile());
  }, [dispatch]);

  function handleSave() {
    dispatch(
      editProfile({
        nickname: nickname || null,
        image: image || null,
      })
    );
    setEditing(false);
  }

  function handleToggleFriends() {
    if (!showFriends) {
      dispatch(loadFriends());
    }
    setShowFriends(prev => !prev);
  }

  if (loading || !user) {
    return <p style={{ padding: 16 }}>Carregando perfil...</p>;
  }

  return (
    <section className="profile-container">
      <div className="profile-header">
        <img
          className="profile-avatar"
          src={
            user.image ??
            "https://voxnews.com.br/wp-content/uploads/2017/04/unnamed.png"
          }
        />

        <h2>{user.name}</h2>
        <p><strong>Email: </strong> {user.email}</p>
        <p>@{user.nickName}</p>

        <div className="profile-stats">
         
          <button
            className="friends-toggle-btn"
            
          >
             {user.postsCount} posts
          </button>

          <button
            className="friends-toggle-btn"
            onClick={handleToggleFriends}
          >
             {user.friendsCount} amigos
          </button>
        </div>

        <button onClick={() => setEditing(!editing)}>
          Editar perfil
        </button>
      </div>

      {editing && (
        <div className="profile-edit">
          <input
            placeholder="Novo nickname"
            value={nickname}
            onChange={e => setNickname(e.target.value)}
          />

          <input
            placeholder="URL da imagem"
            value={image}
            onChange={e => setImage(e.target.value)}
          />

          <button onClick={handleSave}>Salvar</button>
        </div>
      )}

      {showFriends && (
        <div className="friends-list">
          <h3>Amigos</h3>

          {friends.map(item => (
            <div
              key={item.friend.idFriend}
              className="friend-item"
            >
              <img
                src={
                  item.friend.imageUrl ??
                  "https://voxnews.com.br/wp-content/uploads/2017/04/unnamed.png"
                }
              />
              <span>{item.friend.friendName}</span>
            </div>
          ))}
        </div>
      )}
    </section>
  );
}
