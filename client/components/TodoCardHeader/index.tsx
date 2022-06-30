import { Grid, Row, Text } from "@nextui-org/react";
import React, { Dispatch } from "react";
import { CreateTodoCommand } from "../../types";
import AddSubtaskFormModal from "../AddSubtaskFormModal";
import { EditIconButton } from "../Buttons";
import FilterBar, { ShowFilterType } from "../FilterBar";
import TodoFormModal from "../TodoFormModal";

export interface TodoCardHeaderProps {
  current: ShowFilterType;
  setCurrent: Dispatch<React.SetStateAction<ShowFilterType>>;
  id: number;
  title: string;
  projectId: number;

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
  projectId,
}: TodoCardHeaderProps) => {
  return (
    <Grid.Container justify='space-between'>
      <Row justify='space-between' align='center' css={{ p: "$0 $3" }}>
        <Text b h4 css={{ width: "50%" }}>
          {title}
        </Text>
        <Grid.Container alignItems='center' justify='flex-end'>
          <Grid css={{ padding: "$0" }}>
            <AddSubtaskFormModal todoId={id} projectId={projectId} />
          </Grid>
          <Grid css={{ padding: "$0" }}>
            <TodoFormModal
              buttonTitle='Update'
              title='Update todo item'
              propsValues={initialFormData}
              onSubmit={statusUpdateHandler}
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
