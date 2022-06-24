import { Card, Row, Button, Text, Tooltip } from "@nextui-org/react";
import React, { useContext } from "react";
import { ProgressBar, SubtaskResponseDto } from "../../types";
import { AuthContextInterface, AuthContext } from "../../Contexts/Auth";
("../../types");

export interface SubtaskItemProps extends Omit<SubtaskResponseDto, "todo"> {}

const Index = ({ id, title, status, description }: SubtaskItemProps) => {
  const { token } = useContext<AuthContextInterface>(AuthContext);

  return (
    <Card>
      <Card.Header css={{ display: "flex", justifyContent: "space-between" }}>
        <Text b>{title}</Text>
        <Tooltip content={"status"}>
          <Button auto flat>
            {ProgressBar[status]}
          </Button>
        </Tooltip>
      </Card.Header>
      <Card.Divider />
      <Card.Body>
        <Text>{description}</Text>
      </Card.Body>

      <Card.Divider />
      <Card.Footer>
        <Row justify='space-around' align='center'>
          <Button size='sm' color='error'>
            Delete
          </Button>
        </Row>
      </Card.Footer>
    </Card>
  );
};

export default Index;
