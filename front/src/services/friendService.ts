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

export async function fetchFriendRequests() {
  const token = localStorage.getItem("token");

  const response = await fetch(`${API_URL}/requests`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Erro ao buscar solicitações");
  }

  return response.json();
}

export async function respondFriendRequest(
  friendId: number,
  status: number
) {
  const token = localStorage.getItem("token");

  const response = await fetch(`${API_URL}/accept`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify({
      friendId,
      status,
    }),
  });

  if (!response.ok) {
    throw new Error("Erro ao responder solicitação");
  }

  return response.json();
}
