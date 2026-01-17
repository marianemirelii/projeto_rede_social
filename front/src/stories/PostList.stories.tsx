import type { Meta, StoryObj } from "@storybook/react";
import { PostList } from "./../components/PostList/PostList";
import { postsMock } from "./../mocks/postsMock";

const meta: Meta<typeof PostList> = {
  title: "RedeSocial/PostList",
  component: PostList,
};

export default meta;

type Story = StoryObj<typeof PostList>;

export const Padrao: Story = {
  args: {
    posts: postsMock,
  },
};

export const FeedVazio: Story = {
  args: {
    posts: [],
  },
};
