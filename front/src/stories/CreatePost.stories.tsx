import type { Meta, StoryObj } from "@storybook/react";
import { CreatePost } from "./../components/CreatePost/CreatePost";

const meta: Meta<typeof CreatePost> = {
  title: "RedeSocial/CreatePost",
  component: CreatePost,
};

export default meta;
type Story = StoryObj<typeof CreatePost>;

export const Padrao: Story = {
  args: {
    aoPublicar: (conteudo, isPublic) => {
      console.log("Novo post:", { conteudo, isPublic });
    },
  },
};
