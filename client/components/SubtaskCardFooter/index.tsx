import { Grid, Text } from "@nextui-org/react";
import React from "react";
import { DeleteIconButton, DoneIconButton } from "../Buttons";

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
        css={{ p: "$1 $4" }}
        onClick={() => statusUpdateHandler(2)}
      >
        <DoneIconButton
          iconWidth={30}
          iconHeight={30}
          onSubmitHandler={() => statusUpdateHandler(2)}
        />
        <Text h5>Done</Text>
      </Grid.Container>
    );

  return <></>;
};

export default Index;
