import { Button, Grid, Row, Text, Tooltip } from "@nextui-org/react";
import React from "react";
import { CreateSubtaskCommand } from "../../types";
import {
  StartIconButton,
  OffIconButton,
  EditIconButton,
  RestartIconButton,
} from "../Buttons";
import SubtaskFormModal from "../SubtaskFormModal";

export interface SubtaskCardHeaderProps {
  projectId: number;
  title: string;
  isCompleted: boolean;
  isStarted: boolean;
  initialFormData: CreateSubtaskCommand;
  statusUpdateHandler: (status: number) => void;
  addSubtaskHandler: (values: CreateSubtaskCommand) => Promise<void>;
}

const Index = ({
  projectId,
  isCompleted,
  initialFormData,
  title,
  statusUpdateHandler,
  addSubtaskHandler,
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
          <Grid.Container alignItems='center' justify='flex-end' gap={1}>
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
              <SubtaskFormModal
                formInitValues={initialFormData}
                projectId={projectId}
                buttonTitle='Update'
                title='Update subtask'
                onSubmit={addSubtaskHandler}
                actionButton={<EditIconButton iconWidth={20} iconHeight={20} />}
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
