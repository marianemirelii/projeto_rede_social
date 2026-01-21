import type { Meta, StoryObj } from "@storybook/react";
import { Header } from "./../components/Header/Header";
import { userMock } from "../mocks/userMock";

const meta: Meta<typeof Header> = {
  title: "RedeSocial/Header",
  component: Header,
};

export default meta;

export const Padrao: StoryObj<typeof Header> = {
  args: {
    user: userMock,
  },
};
