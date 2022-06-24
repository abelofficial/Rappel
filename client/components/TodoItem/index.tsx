import { Card, Row, Button, Text, Tooltip } from "@nextui-org/react";
import React, { useContext } from "react";
import { ProgressBar, TodoResponseDto } from "../../types";
import useSWR from "swr";
import { getUserTodoSubtasksListQuery } from "../../services/Queries";
import { AuthContextInterface, AuthContext } from "../../Contexts/Auth";
("../../types");

export interface TodoItemProps extends Omit<TodoResponseDto, "user"> {}

const Index = ({ id, title, status, description }: TodoItemProps) => {
  const { token } = useContext<AuthContextInterface>(AuthContext);

  const { data, error } = useSWR(`/todo/${id}/todossubtasks`, () =>
    getUserTodoSubtasksListQuery(token + "", id)
  );

  const displaySubTasks = () => {
    if (!data) return <div>loading subtasks...</div>;

    if (data.data.length === 0) return <div>No subtasks</div>;

    return <>{data.data.length} subtasks</>;
  };
  return (
    <Card>
      <Card.Header>
        <Text b>{title}</Text>
      </Card.Header>
      <Card.Divider />
      <Card.Body>
        <Text>{description}</Text>
        {displaySubTasks()}
      </Card.Body>

      <Card.Divider />
      <Card.Footer>
        <Row justify='space-around' align='center'>
          <Tooltip content={Object.values(status)[0]}>
            <Button auto flat>
              {ProgressBar[status]}
            </Button>
          </Tooltip>
          <Button size='sm' color='primary'>
            Learn more
          </Button>
        </Row>
      </Card.Footer>
    </Card>
  );
};

export default Index;
