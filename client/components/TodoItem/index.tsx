import { Text, Collapse } from "@nextui-org/react";
import React, { useContext, useState } from "react";
import {
  CreateTodoCommand,
  ProgressBar,
  SubtaskResponseDto,
  TodoResponseDto,
} from "../../types";
import useSWR, { useSWRConfig } from "swr";
import { getUserTodoItemQuery } from "../../services/Queries";
import { AuthContextInterface, AuthContext } from "../../Contexts/Auth";
import * as Gateway from "../../services/QueriesGateway";
import { UserTodoItemURL } from "../../services/QueriesGateway";
import { updateTodoCommand } from "../../services/commands";
import FilteredSubtaskList from "../FilteredSubtaskList";
import TodoCardHeader from "../TodoCardHeader";
import Card from "../Card";
("../../types");

export interface TodoItemProps extends Omit<TodoResponseDto, "subTask"> {
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

  if (!data) return <div>loading subtasks...</div>;

  const getCompletedCount = () =>
    data.subTask.reduce(
      (p: number, c: SubtaskResponseDto) =>
        c.status === ProgressBar.COMPLETED ? (p += 1) : p,
      0
    );
  const getStartedCount = () =>
    data.subTask.reduce(
      (p: number, c: SubtaskResponseDto) =>
        c.status === ProgressBar.STARTED ? (p += 1) : p,
      0
    );

  const onChangeHandler = (value?: ShowFilterType) => {
    value && setCurrentShowing(value);
    value && mutate(Gateway.UserTodoSubtasksListURL(id), data.subTask, true);
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

  return (
    <Card
      body={
        <>
          <Text css={{ padding: "$4" }}>{data.description}</Text>
          {data.subTask.length !== 0 && (
            <Collapse
              css={{ padding: "$0 $5" }}
              title={
                <Text h5 transform='capitalize'>
                  {data.subTask.length +
                    " " +
                    (data.subTask.length === 1 ? "subtask" : "subtasks")}
                </Text>
              }
              bordered
              subtitle={`${getStartedCount()} on going | ${getCompletedCount()} competed`}
            >
              <FilteredSubtaskList
                id={id}
                currentShowing={currentShowing}
                onFilterChange={onChangeHandler}
              />
            </Collapse>
          )}
        </>
      }
      headerContent={
        <TodoCardHeader
          current={currentShowing}
          setCurrent={setCurrentShowing}
          id={id}
          title={data.title}
          initialFormData={data}
          showFilterBar={data.subTask.length > 0}
          statusUpdateHandler={onUpdateTodoHandler}
          projectId={projectId}
        />
      }
      status={data.status}
      showStatus={false}
    />
  );
};

export default Index;
