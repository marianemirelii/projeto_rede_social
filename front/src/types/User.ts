export interface User {
  id: number;
  name: string;
  email: string;
  birthDate: string;
  cep: string;
  image: string | null;
  nickName: string;
  postsCount: number;
  friendsCount: number;
}
