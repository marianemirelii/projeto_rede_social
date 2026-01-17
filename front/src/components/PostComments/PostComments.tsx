import { useState } from "react";
import type { Comment } from "../../types/Comment";
import "./PostComments.css";

interface PostCommentsProps {
  comments: Comment[];
}

export function PostComments({ comments }: PostCommentsProps) {
  const [commentList, setCommentList] = useState<Comment[]>(comments);
  const [newComment, setNewComment] = useState("");

  function handleAddComment() {
    if (!newComment.trim()) return;

    const comment: Comment = {
      id: Date.now(),
      postId: comments[0]?.postId ?? 0,
      userId: 1,
      userName: "Você",
      ImageUser: null,
      content: newComment,
      createdAt: new Date().toISOString(),
    };

    setCommentList((prev) => [...prev, comment]);
    setNewComment("");
  }

  return (
    <section className="post-comments">
      <ul className="comment-list">
        {commentList.map((comment) => (
          <li key={comment.id} className="comment-item">
            <div className="comment-header">
              <strong>{comment.userName}</strong>
              <span className="comment-date">
                {new Date(comment.createdAt).toLocaleString("pt-BR")}
              </span>
            </div>
            <p>{comment.content}</p>
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
