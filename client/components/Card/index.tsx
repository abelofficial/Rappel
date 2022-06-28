import { Card } from "@nextui-org/react";
import React from "react";
import { ProgressBar } from "../../types";

("../../types");

export interface CardProps {
  headerContent: JSX.Element;
  body: JSX.Element;
  status: ProgressBar;
  showStatus: boolean;
  footer?: JSX.Element;
}

const Index = ({
  status,
  headerContent,
  body,
  footer,
  showStatus,
}: CardProps) => {
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
        border: showStatus
          ? `${borderColor()} 0.1em solid`
          : "$border 0.1em solid",
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
      <Card.Body>{body}</Card.Body>

      <Card.Divider />
      {footer && (
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
      )}
    </Card>
  );
};

export default Index;
