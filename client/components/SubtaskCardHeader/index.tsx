import { Button, Grid, Row, Text, Tooltip } from "@nextui-org/react";
import React from "react";
import {
  StartIconButton,
  OffIconButton,
  EditIconButton,
  RestartIconButton,
} from "../Buttons";

export interface SubtaskCardHeaderProps {
  title: string;
  isCompleted: boolean;
  isStarted: boolean;
  statusUpdateHandler: (status: number) => void;
}

const Index = ({
  isCompleted,
  title,
  statusUpdateHandler,
  isStarted,
}: SubtaskCardHeaderProps) => {
  return (
    <Grid.Container alignItems='center' justify='space-between'>
      <Grid
        xs={12}
        css={{
          display: "flex",
          alignItems: "center",
          justifyContent: "Space-between",
          p: "$0 $3",
        }}
      >
        <Grid.Container>
          <Row>
            <Text b transform='capitalize'>
              {title}
            </Text>
          </Row>
          <Row align='center' css={{ p: "$2 $1" }}>
            <Tooltip content='See all item' contentColor='primary'>
              <Button
                flat
                auto
                size='xs'
                color={isCompleted ? "success" : "warning"}
              >
                {isCompleted ? "completed" : "not completed"}
              </Button>
            </Tooltip>
          </Row>
        </Grid.Container>
        {!isCompleted ? (
          <Grid.Container justify='flex-end' gap={1}>
            <Grid css={{ padding: "$0 $2" }}>
              {!isStarted ? (
                <StartIconButton
                  iconWidth={17}
                  iconHeight={17}
                  onSubmitHandler={() => statusUpdateHandler(1)}
                />
              ) : (
                <OffIconButton
                  iconWidth={17}
                  iconHeight={17}
                  onSubmitHandler={() => statusUpdateHandler(0)}
                />
              )}
            </Grid>
            <Grid css={{ padding: "$0 $2" }}>
              <EditIconButton
                iconWidth={17}
                iconHeight={17}
                onSubmitHandler={() => {}}
              />
            </Grid>
          </Grid.Container>
        ) : (
          <Grid.Container justify='flex-end' gap={1}>
            <RestartIconButton
              iconWidth={17}
              iconHeight={17}
              onSubmitHandler={() => statusUpdateHandler(0)}
            />
          </Grid.Container>
        )}
      </Grid>
    </Grid.Container>
  );
};

export default Index;
