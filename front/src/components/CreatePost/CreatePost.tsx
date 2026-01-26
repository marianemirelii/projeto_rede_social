import { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../../store";
import { addPost } from "../../store/postsSlice";
import "./CreatePost.css";

export function CreatePost() {
  const dispatch = useDispatch<AppDispatch>();
  const loading = useSelector(
    (state: RootState) => state.posts.loading
  );

  const [content, setContent] = useState("");
  const [isPublic, setIsPublic] = useState(true);
  const [imageUrl, setImageUrl] = useState("");

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();

    if (!content.trim()) return;

    dispatch(
      addPost({
        content,
        isPublic,
        image: imageUrl || null,
      })
    );

    setContent("");
    setImageUrl("");
  }

  return (
    <form className="create-post" onSubmit={handleSubmit}>
      <textarea
        placeholder="O que está acontecendo?"
        value={content}
        onChange={(e) => setContent(e.target.value)}
        maxLength={280}
      />

      <input
        type="url"
        placeholder="URL da imagem (opcional)"
        value={imageUrl}
        onChange={(e) => setImageUrl(e.target.value)}
        className="image-url-input"
      />

      <div className="create-post-footer">
        <label className="checkbox">
          <input
            type="checkbox"
            checked={isPublic}
            onChange={() => setIsPublic(!isPublic)}
          />
          Público
        </label>

        <button disabled={loading || !content.trim()}>
          {loading ? "Publicando..." : "Postar"}
        </button>
      </div>
    </form>
  );
}
