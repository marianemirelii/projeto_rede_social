import type { Comment } from "../types/Comment";

export const commentsMock: Comment[] = [
  {
    id: 3,
    postId: 7,
    userId: 2,
    ImageUser: null,
    userName: "Wenderson Dhomini",
    content: "Obrigado, gostei muito de aprender essa arquitetura",
    createdAt: "2025-12-30T17:17:31.3518934",
  },
  {
    id: 4,
    postId: 7,
    userId: 5,
    ImageUser: null,
    userName: "Luiz Alberto",
    content: "Arquitetura limpa facilita muito a manutenção do sistema",
    createdAt: "2025-12-31T09:10:00.000Z",
  },
];
