export interface Comment {
  id: number;
  postId: number;
  userId: number;
  imageUser: string | null;
  userName: string;
  content: string;
  createdAt: string;
}
