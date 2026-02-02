const API_URL = "http://localhost:5257/api/Friend";

export async function fetchFriends() {
  const token = localStorage.getItem("token");

  const response = await fetch(`${API_URL}/all`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Erro ao buscar amigos");
  }

  return response.json();
}
