import type { Post } from "../types/Post";

const API_URL = "http://localhost:5257/api/Post";

export async function fetchPosts(): Promise<Post[]> {
  const token = localStorage.getItem("token");

  const response = await fetch(`${API_URL}/feed`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Erro ao buscar posts");
  }

  return response.json();
}
