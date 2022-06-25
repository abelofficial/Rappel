import { Grid, Tooltip, Button } from "@nextui-org/react";
import React from "react";

export interface FilterBarProps {
  current: ShowFilterType;
  setter: (value: ShowFilterType) => void;
}

export enum ShowFilterType {
  ALL = "all",
  COMPLETED = "completed",
  STARTED = "started",
}

const Index = ({ current, setter }: FilterBarProps) => {
  return (
    <Grid.Container gap={1}>
      <Grid>
        <Tooltip content='See all item' contentColor='primary'>
          <Button
            flat
            auto
            size='xs'
            color={current === ShowFilterType.ALL ? "success" : "error"}
            onPress={() => setter(ShowFilterType.ALL)}
          >
            All
          </Button>
        </Tooltip>
      </Grid>
      <Grid>
        <Tooltip content='See completed item' contentColor='primary'>
          <Button
            flat
            auto
            size='xs'
            color={current === ShowFilterType.COMPLETED ? "success" : "error"}
            onPress={() => setter(ShowFilterType.COMPLETED)}
          >
            Completed
          </Button>
        </Tooltip>
      </Grid>
      <Grid>
        <Tooltip content='See started item' contentColor='primary'>
          <Button
            flat
            auto
            size='xs'
            color={current === ShowFilterType.STARTED ? "success" : "error"}
            onPress={() => setter(ShowFilterType.STARTED)}
          >
            Started
          </Button>
        </Tooltip>
      </Grid>
    </Grid.Container>
  );
};

export default Index;
