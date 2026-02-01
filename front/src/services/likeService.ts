const API_URL = "http://localhost:5257/api/Like";

export async function likePostApi(postId: number): Promise<boolean> {
  const token = localStorage.getItem("token");

  const response = await fetch(`${API_URL}/${postId}`, {
    method: "POST",
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error(response.statusText || "Erro ao dar like");
  }

  return response.json();
}

export async function unlikePostApi(postId: number): Promise<boolean> {
  const token = localStorage.getItem("token");

  const response = await fetch(`${API_URL}/${postId}`, {
    method: "DELETE",
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error(response.statusText || "Erro ao remover like");
  }

  return response.json();
}
