import { useState } from "react";
import "./CreatePost.css";

interface CreatePostProps {
  aoPublicar?: (conteudo: string, isPublic: boolean) => void;
}

export function CreatePost({ aoPublicar }: CreatePostProps) {
  const [conteudo, setConteudo] = useState("");
  const [isPublic, setIsPublic] = useState(true);

  function handlePublicar() {
    if (!conteudo.trim()) return;

    aoPublicar?.(conteudo, isPublic);

    // mock: limpar formulário após "publicar"
    setConteudo("");
    setIsPublic(true);
  }

  return (
    <section className="create-post">
      <textarea
        className="create-post-textarea"
        placeholder="O que você está pensando?"
        value={conteudo}
        onChange={(e) => setConteudo(e.target.value)}
      />

      <div className="create-post-footer">
        <label className="checkbox">
          <input
            type="checkbox"
            checked={isPublic}
            onChange={() => setIsPublic((prev) => !prev)}
          />
          Público
        </label>

        <button
          className="create-post-button"
          onClick={handlePublicar}
          disabled={!conteudo.trim()}
        >
          Publicar
        </button>
      </div>
    </section>
  );
}
