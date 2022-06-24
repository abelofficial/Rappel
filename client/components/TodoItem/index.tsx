import { Card, Row, Button, Text, Tooltip } from "@nextui-org/react";
import React from "react";
import { ProgressBar, TodoResponseDto } from "../../types";

export interface TodoItemProps extends Omit<TodoResponseDto, "user"> {}

const Index = ({ title, status, description }: TodoItemProps) => {
  return (
    <Card>
      <Card.Header>
        <Text b>{title}</Text>
      </Card.Header>
      <Card.Divider />
      <Card.Body>
        <Text>{description}</Text>
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
