import type { User } from "../types/User";

const API_URL = "http://localhost:5257/api/User";

export async function fetchLoggedUser(token: string): Promise<User> {
  const response = await fetch(API_URL, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Erro ao buscar usuário");
  }

  return response.json();
}

export async function fetchProfile() {
  const token = localStorage.getItem("token");

  const response = await fetch(API_URL, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Erro ao buscar perfil");
  }

  return response.json();
}

export async function updateProfile(data: {
  nickname?: string | null;
  image?: string | null;
}) {
  const token = localStorage.getItem("token");

  const response = await fetch(API_URL, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    throw new Error("Erro ao atualizar perfil");
  }

  return response.json();
}
