export interface Post {
  id: number;
  userId: number;
  userName: string;
  imageUser: string | null;
  content: string;
  isPublic: boolean;
  image: string | null;
  createdAt: string;
  likes: number;
  comments: number;
}

export interface CreatePostRequest {
  content: string;
  isPublic: boolean;
  image: string | null;
}
