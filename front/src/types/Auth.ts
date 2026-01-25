export interface RegisterRequest {
  name: string;
  email: string;
  password: string;
  birthDate: string;
  cep: string;
  nickName: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

    export interface LoginResponse {
  token: string;
}