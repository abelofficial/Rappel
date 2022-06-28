import { Card, Text, Grid, Collapse, Row } from "@nextui-org/react";
import React, { useContext, useState } from "react";
import {
  CreateTodoCommand,
  ProgressBar,
  SubtaskResponseDto,
  TodoResponseDto,
} from "../../types";
import useSWR, { useSWRConfig } from "swr";
import {
  getUserTodoItemQuery,
  getUserTodoSubtasksListQuery,
} from "../../services/Queries";
import { AuthContextInterface, AuthContext } from "../../Contexts/Auth";
import SubtaskItem from "../SubtaskItem";
import AddSubtaskFormModal from "../AddSubtaskFormModal";
import FilterBar from "../FilterBar";
import * as Gateway from "../../services/QueriesGateway";
import { UserTodoItemURL } from "../../services/QueriesGateway";
import TodoFormModal from "../TodoFormModal";
import { updateTodoCommand } from "../../services/commands";
import { EditIconButton, SettingIconButton } from "../Buttons";
("../../types");

export interface TodoItemProps extends Omit<TodoResponseDto, "user"> {
  projectId: number;
}

export enum ShowFilterType {
  ALL = "all",
  STARTED = "started",
  COMPLETED = "completed",
}

const Index = ({ id, projectId }: TodoItemProps) => {
  const { mutate } = useSWRConfig();
  const [currentShowing, setCurrentShowing] = useState<ShowFilterType>(
    ShowFilterType.ALL
  );
  const { token } = useContext<AuthContextInterface>(AuthContext);
  const { data } = useSWR(UserTodoItemURL(id, projectId), () =>
    getUserTodoItemQuery(token + "", id, projectId)
  );
  const { data: subTasks } = useSWR(Gateway.UserTodoSubtasksListURL(id), () =>
    getUserTodoSubtasksListQuery(token + "", id)
  );

  if (!data || !subTasks) return <div>loading subtasks...</div>;

  const getCompletedCount = () =>
    subTasks.reduce(
      (p: number, c: SubtaskResponseDto) =>
        c.status === ProgressBar.COMPLETED ? (p += 1) : p,
      0
    );
  const getStartedCount = () =>
    subTasks.reduce(
      (p: number, c: SubtaskResponseDto) =>
        c.status === ProgressBar.STARTED ? (p += 1) : p,
      0
    );

  const onChangeHandler = (value?: ShowFilterType) => {
    value && setCurrentShowing(value);
    mutate(Gateway.UserTodoSubtasksListURL(id), subTasks, true);
  };

  const onUpdateTodoHandler = async (values: CreateTodoCommand) => {
    mutate(
      Gateway.UserTodoItemURL(id, projectId),
      async (_d: TodoResponseDto) => {
        const newTodo = await updateTodoCommand(
          token + "",
          id,
          projectId,
          values
        );
        return newTodo;
      }
    );
  };

  const displayFilterSubtaskList = () => {
    switch (currentShowing) {
      case ShowFilterType.ALL:
        return subTasks.map((td) => (
          <Grid xs={12} key={td.id} css={{ padding: "$0" }}>
            <SubtaskItem
              id={td.id}
              parentId={id}
              notifyChange={onChangeHandler}
            />
          </Grid>
        ));

      case ShowFilterType.COMPLETED:
        return subTasks
          .filter((td) => td.status === ProgressBar.COMPLETED)
          .map((td) => (
            <Grid xs={12} key={td.id} css={{ padding: "$0" }}>
              <SubtaskItem
                id={td.id}
                parentId={id}
                notifyChange={onChangeHandler}
              />
            </Grid>
          ));
      case ShowFilterType.STARTED:
        return subTasks
          .filter((td) => td.status === ProgressBar.STARTED)
          .map((td) => (
            <Grid xs={12} key={td.id} css={{ padding: "$0" }}>
              <SubtaskItem
                id={td.id}
                parentId={id}
                notifyChange={onChangeHandler}
              />
            </Grid>
          ));

      default:
        break;
    }
  };

  return (
    <Card>
      <Card.Header
        css={{
          display: "flex",
          alignItem: "center",
          justifyContent: "space-between",
        }}
      >
        <Grid.Container justify='space-between'>
          <Row justify='space-between'>
            <Text b h4 css={{ width: "50%" }}>
              {data.title}
            </Text>
            <Grid.Container alignItems='center' justify='flex-end' gap={1}>
              <Grid css={{ padding: "$0 $1" }}>
                <AddSubtaskFormModal todoId={data.id} />
              </Grid>
              <Grid css={{ padding: "$0 $1" }}>
                <TodoFormModal
                  buttonTitle='Update'
                  title='Update todo item'
                  propsValues={data}
                  onSubmit={onUpdateTodoHandler}
                  actionButton={
                    <EditIconButton iconWidth={18} iconHeight={18} />
                  }
                />
              </Grid>
              <Grid css={{ padding: "$0 $2" }}>
                <SettingIconButton iconWidth={18} iconHeight={18} />
              </Grid>
            </Grid.Container>
          </Row>
          <Row>
            {subTasks.length !== 0 && (
              <FilterBar current={currentShowing} setter={onChangeHandler} />
            )}
          </Row>
        </Grid.Container>
      </Card.Header>
      <Card.Divider />
      <Card.Body>
        <Text css={{ padding: "$4" }}>{data.description}</Text>
        {subTasks.length !== 0 && (
          <Collapse
            css={{ padding: "$0 $5" }}
            title={
              <Text h5 transform='capitalize'>
                {subTasks.length +
                  " " +
                  (subTasks.length === 1 ? "subtask" : "subtasks")}
              </Text>
            }
            bordered
            subtitle={`${getStartedCount()} on going | ${getCompletedCount()} competed`}
          >
            {displayFilterSubtaskList()}
          </Collapse>
        )}
      </Card.Body>

      <Card.Divider />
      <Card.Footer
        css={{
          display: "flex",
          alignItem: "center",
          justifyContent: "space-evenly",
        }}
      ></Card.Footer>
    </Card>
  );
};

export default Index;
