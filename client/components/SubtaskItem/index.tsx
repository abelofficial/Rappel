/* eslint-disable react/jsx-no-undef */
import React, { useContext } from "react";
import useSWR, { mutate } from "swr";
import { AuthContextInterface, AuthContext } from "../../Contexts/Auth";
import { updateSubtaskStatusCommand } from "../../services/commands";
import { getUserTodoSubtaskQuery } from "../../services/Queries";
import { ProgressBar, SubtaskResponseDto } from "../../types";
import { ShowFilterType } from "../FilterBar";
import * as Gateway from "../../services/QueriesGateway";
import { UserTodoSubtaskURL } from "../../services/QueriesGateway";

import SubtaskCardHeader from "../SubtaskCardHeader";
import Card from "../Card";
import SubtaskCardFooter from "../SubtaskCardFooter";
import { Text } from "@nextui-org/react";
("../../types");

export interface SubtaskItemProps {
  parentId: number;
  id: number;
  projectId: number;
  notifyChange: (value?: ShowFilterType) => void;
}

const Index = ({ id, parentId, projectId, notifyChange }: SubtaskItemProps) => {
  const { token } = useContext<AuthContextInterface>(AuthContext);
  const { data } = useSWR(UserTodoSubtaskURL(id, parentId, projectId), () =>
    getUserTodoSubtaskQuery(token + "", id, parentId, projectId)
  );

  if (!data) return <h1>loading</h1>;

  const subTaskStatusChangeHandler = (status: number) => {
    console.log("S: ", status);
    mutate(
      Gateway.UserTodoSubtaskURL(id, parentId, projectId),
      async (_d: SubtaskResponseDto) => {
        const updated = await updateSubtaskStatusCommand(
          token + "",
          parentId,
          id,
          {
            status,
          }
        );
        notifyChange();
        return updated;
      }
    );
  };

  return (
    <Card
      status={data.status}
      headerContent={
        <SubtaskCardHeader
          title={data.title}
          isStarted={data.status === ProgressBar.STARTED}
          isCompleted={data.status === ProgressBar.COMPLETED}
          statusUpdateHandler={subTaskStatusChangeHandler}
        />
      }
      body={<Text css={{ padding: "$4" }}> {data.description}</Text>}
      footer={
        data.status !== ProgressBar.CREATED ? (
          <SubtaskCardFooter
            isStarted={data.status === ProgressBar.STARTED}
            isCompleted={data.status === ProgressBar.COMPLETED}
            statusUpdateHandler={subTaskStatusChangeHandler}
          />
        ) : undefined
      }
      showStatus={true}
    />
  );
};

export default Index;
