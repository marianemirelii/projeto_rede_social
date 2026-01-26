import type { Post, CreatePostRequest } from "../types/Post";

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

export async function createPost(
  data: CreatePostRequest
): Promise<Post> {
  const token = localStorage.getItem("token");

  const response = await fetch(API_URL, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    throw new Error("Erro ao criar post");
  }

  return response.json();
}
