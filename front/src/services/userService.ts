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
