import { useSelector } from "react-redux";
import type { RootState } from "../../store";
import { PostCard } from "../PostCard/PostCard";
import "./styles.css";

export function PostList() {
  const { posts, loading, error } = useSelector(
    (state: RootState) => state.posts
  );

  if (loading) {
    return <p className="feed-vazio">Carregando feed...</p>;
  }

  if (error) {
    return <p className="feed-vazio">{error}</p>;
  }

  if (posts.length === 0) {
    return <p className="feed-vazio">Nenhum post encontrado</p>;
  }

  return (
    <section className="feed-container">
      {posts.map(post => (
        <PostCard key={post.id} post={post} />
      ))}
    </section>
  );
}
