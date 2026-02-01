const API_URL = "http://localhost:5257/api/Post";

export async function fetchComments(postId: number) {
  const token = localStorage.getItem("token");

  const response = await fetch(`${API_URL}/${postId}/comments`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Erro ao buscar comentários");
  }

  return response.json();
}

export async function createComment(postId: number, content: string) {
  const token = localStorage.getItem("token");

  const response = await fetch(`${API_URL}/${postId}/comments`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify({ content }),
  });

  if (!response.ok) {
    throw new Error("Erro ao criar comentário");
  }

  return response.json();
}
