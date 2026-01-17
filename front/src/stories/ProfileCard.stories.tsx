import type { Meta, StoryObj } from "@storybook/react";
import { ProfileCard } from "./../components/ProfileCard/ProfileCard";
import { userMock } from "./../mocks/userMock";

const meta: Meta<typeof ProfileCard> = {
  title: "RedeSocial/ProfileCard",
  component: ProfileCard,
  parameters: {
    backgrounds: {
      default: "dark",
    },
  },
};

export default meta;

type Story = StoryObj<typeof ProfileCard>;

export const Padrao: Story = {
  args: {
    user: userMock,
  },
};
