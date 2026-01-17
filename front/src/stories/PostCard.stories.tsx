import type { Meta, StoryObj } from "@storybook/react";
import { PostCard } from "../components/PostCard/PostCard";
import type { Post } from "../types/Post";

const postMock: Post = {
  id: 1,
  userId: 5,
  userName: "Luiz Alberto",
  userImage: "https://i.pravatar.cc/150?img=3",
  content:
    "Essa separação de Domain, Application e Infrastructure ficou bem organizada. Curti bastante!",
  isPublic: true,
  image: null,
  createdAt: "2025-12-30T17:15:23.307Z",
  likes: 12,
  comments: 3,
};

const meta: Meta<typeof PostCard> = {
  title: "RedeSocial/PostCard",
  component: PostCard,
  decorators: [
    (Story) => (
      <div
        style={{
          maxWidth: 600,
          margin: "0 auto",
          border: "1px solid #e6ecf0",
        }}
      >
        <Story />
      </div>
    ),
  ],
};

export default meta;
type Story = StoryObj<typeof PostCard>;

export const Padrao: Story = {
  args: {
    post: postMock,
  },
};

export const ComImagem: Story = {
  args: {
    post: {
      ...postMock,
      image: "https://picsum.photos/600/300",
    },
  },
};
