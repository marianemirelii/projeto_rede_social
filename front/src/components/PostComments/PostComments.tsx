import { useEffect, useState } from "react";
import type { Comment } from "../../types/Comment";
import { fetchComments, createComment } from "../../services/commentService";
import "./PostComments.css";

interface PostCommentsProps {
  postId: number;
}

export function PostComments({ postId }: PostCommentsProps) {
  const [commentList, setCommentList] = useState<Comment[]>([]);
  const [newComment, setNewComment] = useState("");
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    loadComments();
  }, [postId]);

  async function loadComments() {
    try {
      setLoading(true);
      const data = await fetchComments(postId);
      setCommentList(data);
    } catch (error) {
      console.error("Erro ao buscar comentários");
    } finally {
      setLoading(false);
    }
  }

  async function handleAddComment() {
    if (!newComment.trim()) return;

    try {
      await createComment(postId, newComment);
      setNewComment("");
      loadComments();
    } catch {
      console.error("Erro ao comentar");
    }
  }

  return (
    <section className="post-comments">
      {loading && <p style={{ color: "#71767b" }}>Carregando comentários...</p>}

      <ul className="comment-list">
        {commentList.map((comment) => (
          <li key={comment.id} className="comment-item">
            <div className="comment-avatar">
              <img
                src={
                  comment.imageUser ??
                  "https://voxnews.com.br/wp-content/uploads/2017/04/unnamed.png"
                }
                alt={comment.userName}
              />
            </div>

            <div className="comment-body">
              <div className="comment-header">
                <strong>{comment.userName}</strong>
                <span className="comment-date">
                  {new Date(comment.createdAt).toLocaleString("pt-BR")}
                </span>
              </div>

              <p>{comment.content}</p>
            </div>
          </li>
        ))}
      </ul>

      <div className="comment-form">
        <input
          type="text"
          placeholder="Escreva um comentário..."
          value={newComment}
          onChange={(e) => setNewComment(e.target.value)}
        />
        <button onClick={handleAddComment}>Comentar</button>
      </div>
    </section>
  );
}
