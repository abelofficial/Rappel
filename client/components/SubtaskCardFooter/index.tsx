import { Button, Grid } from "@nextui-org/react";
import React from "react";
import { DeleteIconButton } from "../Buttons";

export interface SubtaskCardFooterProps {
  isCompleted: boolean;
  isStarted: boolean;
  statusUpdateHandler: (status: number) => void;
}

const Index = ({
  isCompleted,
  statusUpdateHandler,
  isStarted,
}: SubtaskCardFooterProps) => {
  if (isCompleted)
    return (
      <DeleteIconButton
        iconWidth={30}
        iconHeight={30}
        onSubmitHandler={() => {}}
      />
    );

  if (isStarted)
    return (
      <Grid.Container
        alignItems='center'
        justify='flex-end'
        css={{ p: "$1" }}
        onClick={() => statusUpdateHandler(2)}
      >
        <Button
          rounded
          bordered
          auto
          color='success'
          onClick={() => statusUpdateHandler(2)}
        >
          Done
        </Button>
      </Grid.Container>
    );

  return <></>;
};

export default Index;
