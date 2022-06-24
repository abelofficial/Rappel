import {
  Card,
  Row,
  Button,
  Text,
  Tooltip,
  Grid,
  Collapse,
} from "@nextui-org/react";
import React, { useContext } from "react";
import { ProgressBar, TodoResponseDto } from "../../types";
import useSWR from "swr";
import { getUserTodoSubtasksListQuery } from "../../services/Queries";
import { AuthContextInterface, AuthContext } from "../../Contexts/Auth";
import SubtaskItem from "../SubtaskItem";
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

    return (
      <Collapse title='' subtitle={`${data.data.length} total subtasks`}>
        {data.data?.map((td) => (
          <Grid xs={12} key={td.id}>
            <SubtaskItem
              id={td.id}
              title={td.title}
              description={td.description}
              status={td.status}
            />
          </Grid>
        ))}
      </Collapse>
    );
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
        <Text b>{title}</Text>
        <Tooltip content={Object.values(status)[0]}>
          <Button auto flat>
            {ProgressBar[status]}
          </Button>
        </Tooltip>
      </Card.Header>
      <Card.Divider />
      <Card.Body>
        <Text>{description}</Text>
        {displaySubTasks()}
      </Card.Body>

      <Card.Divider />
      <Card.Footer>
        <Row justify='space-around' align='center'>
          <Button size='sm' color='error' disabled={data?.data.length != 0}>
            Delete
          </Button>
          <Button size='sm' color='success'>
            New subtask
          </Button>
        </Row>
      </Card.Footer>
    </Card>
  );
};

export default Index;
