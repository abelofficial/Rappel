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
import Statusbar from "../Statusbar";
import * as Gateway from "../../services/QueriesGateway";
import { UserTodoSubtaskURL } from "../../services/QueriesGateway";
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

  return (
    <Card>
      <Card.Header>
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
                <Grid>
                  {data.status === ProgressBar.CREATED ? (
                    <Image
                      src='/play-icon.svg'
                      alt='start-icon'
                      width={20}
                      height={20}
                      onClick={() => subTaskStatusChangeHandler(1)}
                    />
                  ) : (
                    <Image
                      src='/turnoff-icon.svg'
                      alt='restart-icon'
                      width={20}
                      height={20}
                      onClick={() => subTaskStatusChangeHandler(0)}
                    />
                  )}
                </Grid>
                <Grid>
                  <Image
                    src='/edit-icon.svg'
                    alt='An SVG of an eye'
                    width={20}
                    height={20}
                  />
                </Grid>
                <Grid>
                  <Image
                    src='/settings-icon.svg'
                    alt='An SVG of an eye'
                    width={20}
                    height={20}
                  />
                </Grid>
              </Grid.Container>
            ) : (
              <Grid.Container justify='flex-end' gap={1}>
                <Image
                  src='/restart-icon.svg'
                  alt='start-icon'
                  width={20}
                  height={20}
                  onClick={() => subTaskStatusChangeHandler(0)}
                />
              </Grid.Container>
            )}
          </Grid>
          <Grid xs={12}>
            <Statusbar
              status={data.status}
              onStart={() => subTaskStatusChangeHandler(1)}
              markAsDone={() => subTaskStatusChangeHandler(2)}
            />
          </Grid>
        </Grid.Container>
      </Card.Header>
      <Card.Divider />
      <Card.Body>
        <Text>{data.description}</Text>
      </Card.Body>

      <Card.Divider />
      <Card.Footer css={{ display: "flex", justifyContent: "center" }}>
        {data.status === ProgressBar.COMPLETED && (
          <Button size='sm' color='error'>
            Archive
          </Button>
        )}
      </Card.Footer>
    </Card>
  );
};

export default Index;
