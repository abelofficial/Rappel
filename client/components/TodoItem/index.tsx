import { Card, Text, Grid, Collapse } from "@nextui-org/react";
import React, { useContext, useState } from "react";
import { ProgressBar, SubtaskResponseDto, TodoResponseDto } from "../../types";
import useSWR, { useSWRConfig } from "swr";
import {
  getUserTodoItemQuery,
  getUserTodoSubtasksListQuery,
} from "../../services/Queries";
import { AuthContextInterface, AuthContext } from "../../Contexts/Auth";
import SubtaskItem from "../SubtaskItem";
import AddSubtaskFormModal from "../AddSubtaskFormModal";
import FilterBar from "../FilterBar";
("../../types");

export interface TodoItemProps extends Omit<TodoResponseDto, "user"> {
  projectId: number;
}

export enum ShowFilterType {
  ALL = "all",
  COMPLETED = "completed",
  STARTED = "started",
}

const Index = ({ id, projectId }: TodoItemProps) => {
  const { mutate } = useSWRConfig();
  const [currentShowing, setCurrentShowing] = useState<ShowFilterType>(
    ShowFilterType.ALL
  );
  const { token } = useContext<AuthContextInterface>(AuthContext);
  const { data, error } = useSWR(`project/${projectId}/todo/${id}`, () =>
    getUserTodoItemQuery(token + "", id, projectId)
  );
  const tasks = useSWR(`/todo/${id}/todossubtasks`, () =>
    getUserTodoSubtasksListQuery(token + "", id)
  );
  console.log(error);
  const subTasks = tasks.data;

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
    mutate(`/todo/${id}/todossubtasks`, subTasks, true);
  };

  const displayFilterSubtaskList = () => {
    switch (currentShowing) {
      case ShowFilterType.ALL:
        return subTasks.map((td) => (
          <Grid xs={12} key={td.id}>
            <SubtaskItem id={td.id} parentId={id} onChange={onChangeHandler} />
          </Grid>
        ));

      case ShowFilterType.COMPLETED:
        return subTasks
          .filter((td) => td.status === ProgressBar.COMPLETED)
          .map((td) => (
            <Grid xs={12} key={td.id}>
              <SubtaskItem
                id={td.id}
                parentId={id}
                onChange={onChangeHandler}
              />
            </Grid>
          ));
      case ShowFilterType.STARTED:
        return subTasks
          .filter((td) => td.status === ProgressBar.STARTED)
          .map((td) => (
            <Grid xs={12} key={td.id}>
              <SubtaskItem
                id={td.id}
                parentId={id}
                onChange={onChangeHandler}
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
          <Grid>
            <Text b>{data.title}</Text>
          </Grid>
          <Grid>
            <AddSubtaskFormModal todoId={data.id} />
          </Grid>
        </Grid.Container>
      </Card.Header>
      <Card.Divider />
      <Card.Body>
        {subTasks.length !== 0 && (
          <FilterBar current={currentShowing} setter={onChangeHandler} />
        )}
        <Text>{data.description}</Text>
        {subTasks.length !== 0 && (
          <Collapse
            title='Sub tasks'
            subtitle={`${
              getCompletedCount() + "/" + subTasks.length
            } competed | ${getStartedCount() + "/" + subTasks.length} on going`}
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
