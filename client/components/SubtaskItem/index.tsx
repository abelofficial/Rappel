/* eslint-disable react/jsx-no-undef */
import Image from "next/image";
import { Card, Button, Text, Grid, Row } from "@nextui-org/react";
import React, { useContext } from "react";
import useSWR, { mutate } from "swr";
import { AuthContextInterface, AuthContext } from "../../Contexts/Auth";
import { updateSubtaskStatusCommand } from "../../services/commands";
import { getUserTodoSubtaskQuery } from "../../services/Queries";
import { ProgressBar } from "../../types";
import { ShowFilterType } from "../FilterBar";
import * as Gateway from "../../services/QueriesGateway";
import { UserTodoSubtaskURL } from "../../services/QueriesGateway";
import {
  EditIconButton,
  OffIconButton,
  RestartIconButton,
  SettingIconButton,
  StartIconButton,
} from "../Buttons";
("../../types");

export interface SubtaskItemProps {
  parentId: number;
  id: number;
  onChange: (value?: ShowFilterType) => void;
}

const Index = ({ id, parentId, onChange }: SubtaskItemProps) => {
  const { token } = useContext<AuthContextInterface>(AuthContext);
  const { data } = useSWR(UserTodoSubtaskURL(id, parentId), () =>
    getUserTodoSubtaskQuery(token + "", id, parentId)
  );

  if (!data) return <h1>loading</h1>;

  const subTaskStatusChangeHandler = async (status: number) => {
    await updateSubtaskStatusCommand(token + "", parentId, id, {
      status,
    });
    mutate(Gateway.UserTodoSubtaskURL(id, parentId), { ...data, status });
    onChange();
  };

  const borderColor = () => {
    switch (data.status) {
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
      <Card.Header css={{ p: "$5 $2" }}>
        <Grid.Container alignItems='center' justify='space-between'>
          <Grid
            xs={12}
            css={{
              display: "flex",
              alignItems: "center",
              justifyContent: "Space-between",
            }}
          >
            <Row>
              <Text b>{data.title}</Text>
            </Row>
            {data.status !== ProgressBar.COMPLETED ? (
              <Grid.Container justify='flex-end' gap={1}>
                <Grid css={{ padding: "$0 $2" }}>
                  {data.status === ProgressBar.CREATED ? (
                    <StartIconButton
                      iconWidth={17}
                      iconHeight={17}
                      onSubmitHandler={() => subTaskStatusChangeHandler(1)}
                    />
                  ) : (
                    <OffIconButton
                      iconWidth={17}
                      iconHeight={17}
                      onSubmitHandler={() => subTaskStatusChangeHandler(0)}
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
                <Grid css={{ padding: "$0 $2" }}>
                  <SettingIconButton
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
                  onSubmitHandler={() => subTaskStatusChangeHandler(0)}
                />
              </Grid.Container>
            )}
          </Grid>
        </Grid.Container>
      </Card.Header>
      <Card.Divider />
      <Card.Body>
        <Text>{data.description}</Text>
      </Card.Body>

      <Card.Divider />
      <Card.Footer
        css={{ display: "flex", justifyContent: "center", p: "$3 $2" }}
      >
        {data.status === ProgressBar.COMPLETED && (
          <Button size='sm' color='error'>
            Archive
          </Button>
        )}
        {data.status === ProgressBar.STARTED && (
          <Grid.Container justify='flex-end'>
            <Image
              src='/done-icon.svg'
              alt='start-icon'
              width={23}
              height={30}
              onClick={() => subTaskStatusChangeHandler(2)}
            />
            <Text h4 css={{ p: "$0 $2", color: "green" }}>
              Done
            </Text>
          </Grid.Container>
        )}
      </Card.Footer>
    </Card>
  );
};

export default Index;
