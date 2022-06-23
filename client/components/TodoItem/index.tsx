import { Card, Row, Button, Text } from "@nextui-org/react";
import React from "react";
import { TodoResponseDto } from "../../types";

export interface TodoItemProps extends Omit<TodoResponseDto, "user"> {}

const Index = ({ title, description }: TodoItemProps) => {
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
        <Row justify='flex-end'>
          <Button size='sm' light>
            Share
          </Button>
          <Button size='sm' color='primary'>
            Learn more
          </Button>
        </Row>
      </Card.Footer>
    </Card>
  );
};

export default Index;
