import { Card, Grid, Row, Text } from "@nextui-org/react";
import React, { Dispatch } from "react";
import { CreateTodoCommand } from "../../types";
import AddSubtaskFormModal from "../AddSubtaskFormModal";
import { EditIconButton, SettingIconButton } from "../Buttons";
import FilterBar, { ShowFilterType } from "../FilterBar";
import TodoFormModal from "../TodoFormModal";

export interface TodoCardHeaderProps {
  current: ShowFilterType;
  setCurrent: Dispatch<React.SetStateAction<ShowFilterType>>;
  id: number;
  title: string;

  initialFormData: CreateTodoCommand;
  showFilterBar: boolean;
  statusUpdateHandler: (values: CreateTodoCommand) => Promise<void>;
}

const Index = ({
  current,
  setCurrent,
  id,
  title,
  initialFormData,
  showFilterBar,
  statusUpdateHandler,
}: TodoCardHeaderProps) => {
  return (
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
            {title}
          </Text>
          <Grid.Container alignItems='center' justify='flex-end' gap={1}>
            <Grid css={{ padding: "$0 $1" }}>
              <AddSubtaskFormModal todoId={id} />
            </Grid>
            <Grid css={{ padding: "$0 $1" }}>
              <TodoFormModal
                buttonTitle='Update'
                title='Update todo item'
                propsValues={initialFormData}
                onSubmit={statusUpdateHandler}
                actionButton={<EditIconButton iconWidth={18} iconHeight={18} />}
              />
            </Grid>
            <Grid css={{ padding: "$0 $2" }}>
              <SettingIconButton iconWidth={18} iconHeight={18} />
            </Grid>
          </Grid.Container>
        </Row>
        <Row>
          {showFilterBar && <FilterBar current={current} setter={setCurrent} />}
        </Row>
      </Grid.Container>
    </Card.Header>
  );
};

export default Index;
