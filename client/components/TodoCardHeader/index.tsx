import { Grid, Row, Text } from "@nextui-org/react";
import React, { Dispatch } from "react";
import { CreateSubtaskCommand, CreateTodoCommand } from "../../types";
import { EditIconButton } from "../Buttons";
import FilterBar, { ShowFilterType } from "../FilterBar";
import SubtaskFormModal from "../SubtaskFormModal";
import TodoFormModal from "../TodoFormModal";

export interface TodoCardHeaderProps {
  current: ShowFilterType;
  setCurrent: Dispatch<React.SetStateAction<ShowFilterType>>;
  title: string;
  projectId: number;

  initialFormData: CreateTodoCommand;
  showFilterBar: boolean;
  taskUpdateHandler: (values: CreateTodoCommand) => Promise<void>;

  addSubtaskHandler: (values: CreateSubtaskCommand) => Promise<void>;
}

const Index = ({
  current,
  setCurrent,
  title,
  initialFormData,
  showFilterBar,
  taskUpdateHandler,
  projectId,
  addSubtaskHandler,
}: TodoCardHeaderProps) => {
  return (
    <Grid.Container justify='space-between'>
      <Row justify='space-between' align='center' css={{ p: "$0 $3" }}>
        <Text b h4 css={{ width: "50%" }}>
          {title}
        </Text>
        <Grid.Container alignItems='center' justify='flex-end'>
          <Grid css={{ padding: "$0" }}>
            <SubtaskFormModal
              projectId={projectId}
              buttonTitle='Create'
              title='Create new subtask'
              onSubmit={addSubtaskHandler}
            />
          </Grid>
          <Grid css={{ padding: "$0" }}>
            <TodoFormModal
              buttonTitle='Update'
              title='Update todo item'
              propsValues={initialFormData}
              onSubmit={taskUpdateHandler}
              actionButton={<EditIconButton iconWidth={18} iconHeight={18} />}
            />
          </Grid>
        </Grid.Container>
      </Row>
      <Row>
        {showFilterBar && <FilterBar current={current} setter={setCurrent} />}
      </Row>
    </Grid.Container>
  );
};

export default Index;
