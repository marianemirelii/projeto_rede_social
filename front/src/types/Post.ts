export interface Post {
  id: number;
  userId: number;
  userName: string;
  userImage: string;
  content: string;
  isPublic: boolean;
  image: string | null;
  createdAt: string;
  likes: number;
  comments: number;
}
