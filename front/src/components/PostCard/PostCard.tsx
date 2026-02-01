import { useDispatch } from "react-redux";
import type { AppDispatch } from "../../store";
import { toggleLike } from "../../store/postsSlice";
import type { Post } from "../../types/Post";
import "./styles.css";

interface PostCardProps {
  post: Post;
}

export function PostCard({ post }: PostCardProps) {
  const dispatch = useDispatch<AppDispatch>();

  function handleLike() {
    dispatch(
      toggleLike({
        postId: post.id,
        liked: post.likedByMe,
      })
    );
  }

  return (
    <article className="post-card">
      <div className="post-avatar">
        <img
          src={
            post.imageUser ??
            "https://voxnews.com.br/wp-content/uploads/2017/04/unnamed.png"
          }
          alt={post.userName}
        />
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
          <button
            className={`post-action like ${
              post.likedByMe ? "liked" : ""
            }`}
            onClick={handleLike}
          >
            ❤️ {post.likes}
          </button>

          <span className="post-action">💬 {post.comments}</span>
        </footer>
      </div>
    </article>
  );
}
