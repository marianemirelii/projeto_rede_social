export interface Comment {
  id: number;
  postId: number;
  userId: number;
  userName: string;
  ImageUser: string | null;
  content: string;
  createdAt: string;
}
