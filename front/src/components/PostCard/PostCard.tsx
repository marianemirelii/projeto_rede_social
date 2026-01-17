import { useState } from "react";
import type { Post } from "../../types/Post";
import "./styles.css";

interface PostCardProps {
  post: Post;
}

export function PostCard({ post }: PostCardProps) {
  const [likes, setLikes] = useState(post.likes);

  function handleLike() {
    setLikes((prev) => prev + 1);
  }

  return (
    <article className="post-card">
      <div className="post-avatar">
        {post.userImage ? (
          <img src={post.userImage} alt={post.userName} />
        ) : (
          <div className="post-avatar-placeholder" />
        )}
      </div>

      <div className="post-body">
        <header className="post-header">
          <strong className="post-username">{post.userName}</strong>
          <span className="post-date">
            · {new Date(post.createdAt).toLocaleString("pt-BR")}
          </span>
        </header>

        <p className="post-content">{post.content}</p>

        {post.image && (
          <img
            className="post-image"
            src={post.image}
            alt="Imagem do post"
          />
        )}

        <footer className="post-actions">
          <button className="post-action" onClick={handleLike}>
            ❤️ {likes}
          </button>

          <span className="post-action">💬 {post.comments}</span>
        </footer>
      </div>
    </article>
  );
}
