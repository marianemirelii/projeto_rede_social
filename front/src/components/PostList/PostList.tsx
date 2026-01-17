import type { Post } from "../../types/Post";
import { PostCard } from "../PostCard/PostCard";
import "./styles.css";

interface PostListProps {
  posts: Post[];
}

export function PostList({ posts }: PostListProps) {
  if (posts.length === 0) {
    return <p className="feed-vazio">Nenhum post encontrado</p>;
  }

  return (
    <section className="feed-container">
      {posts.map((post) => (
        <PostCard key={post.id} post={post} />
      ))}
    </section>
  );
}
