import { useDispatch } from "react-redux";
import { likePost } from "../../store/postsSlice";
import type { Post } from "../../types/Post";
import "./styles.css";

interface PostCardProps {
  post: Post;
}

export function PostCard({ post }: PostCardProps) {
  const dispatch = useDispatch();

  function handleLike() {
    dispatch(likePost(post.id));
  }

  return (
    <article className="post-card">
      <div className="post-avatar">
        {post.imageUser ? (
          <img src={post.imageUser} alt={post.userName} />
        ) : (
          <img src="https://voxnews.com.br/wp-content/uploads/2017/04/unnamed.png" alt="imagem padrao" />
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
          <img className="post-image" src={post.image} alt="Imagem do post" />
        )}

        <footer className="post-actions">
          <button className="post-action" onClick={handleLike}>
            ❤️ {post.likes}
          </button>

          <span className="post-action">💬 {post.comments}</span>
        </footer>
      </div>
    </article>
  );
}
