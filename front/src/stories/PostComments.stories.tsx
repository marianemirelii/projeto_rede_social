import type { Meta, StoryObj } from "@storybook/react";
import { PostComments } from "./../components/PostComments/PostComments";
import { commentsMock } from "../mocks/commentsMock";

const meta: Meta<typeof PostComments> = {
  title: "RedeSocial/PostComments",
  component: PostComments,
};

export default meta;

export const Padrao: StoryObj<typeof PostComments> = {
  args: {
    comments: commentsMock,
  },
};
