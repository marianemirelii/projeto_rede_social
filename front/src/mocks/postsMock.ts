import type { Post } from "../types/Post";

export const postsMock: Post[] = [
  {
    id: 3,
    userId: 8,
    userName: "Carlos Lima",
    userImage: "https://i.pravatar.cc/150?img=8",
    content:
      "Comecei a usar React com TypeScript e a diferença na manutenção do código é enorme. Tipagem forte ajuda demais no dia a dia.",
    isPublic: true,
    image: null,
    createdAt: "2026-01-01T12:00:00.000Z",
    likes: 6,
    comments: 2,
  },
  {
    id: 2,
    userId: 2,
    userName: "Maria Souza",
    userImage: "https://i.pravatar.cc/150?img=5",
    content:
      "No Angular, o uso de módulos e injeção de dependência deixa a arquitetura muito organizada, principalmente em projetos grandes.",
    isPublic: true,
    image: null,
    createdAt: "2025-12-31T09:45:10.000Z",
    likes: 9,
    comments: 3,
  },
  {
    id: 1,
    userId: 5,
    userName: "Luiz Alberto",
    userImage: "https://i.pravatar.cc/150?img=3",
    content:
      "Estou estudando Java com Spring Boot e achei muito interessante a separação entre Controller, Service e Repository.",
    isPublic: true,
    image: null,
    createdAt: "2025-12-30T17:15:23.307Z",
    likes: 14,
    comments: 4,
  },
];
