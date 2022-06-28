import { Card, Text } from "@nextui-org/react";
import React from "react";
import { ProgressBar } from "../../types";

("../../types");

export interface CardProps {
  headerContent: JSX.Element;
  bodyText: string;
  status: ProgressBar;
  footer: JSX.Element;
}

const Index = ({ status, headerContent, bodyText, footer }: CardProps) => {
  const borderColor = () => {
    switch (status) {
      case ProgressBar.STARTED:
        return "$successBorder";
      case ProgressBar.CREATED:
        return "$border";
      default:
        return "$errorBorder";
    }
  };

  return (
    <Card
      variant='bordered'
      css={{
        border: `${borderColor()} 0.1em solid`,
      }}
    >
      <Card.Header
        css={{
          display: "flex",
          alignItem: "center",
          justifyContent: "space-between",
          padding: "$1",
        }}
      >
        {headerContent}
      </Card.Header>
      <Card.Divider />
      <Card.Body>
        <Text css={{ padding: "$4" }}> {bodyText}</Text>
      </Card.Body>

      <Card.Divider />
      <Card.Footer
        css={{
          display: "flex",
          alignItem: "center",
          justifyContent: "space-evenly",
          padding: "$1",
        }}
      >
        {footer}
      </Card.Footer>
    </Card>
  );
};

export default Index;
