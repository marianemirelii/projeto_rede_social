// src/services/authService.ts
import type {
  LoginRequest,
  LoginResponse,
  RegisterRequest,
} from "../types/Auth";

const API_URL = "http://localhost:5257/api/User";

async function handleResponse<T>(response: Response): Promise<T> {
  if (!response.ok) {
    const error = await response.text();
    throw new Error(error || "Erro na requisição");
  }

  return response.json() as Promise<T>;
}


export async function registerUser(
  data: RegisterRequest
): Promise<void> {
  const response = await fetch(`${API_URL}/register`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  await handleResponse<void>(response);
}


export async function loginUser(
  data: LoginRequest
): Promise<LoginResponse> {
  const response = await fetch(`${API_URL}/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  return handleResponse<LoginResponse>(response);
}

export function isAuthenticated(): boolean {
  const token = localStorage.getItem("token");
  return !!token;
}