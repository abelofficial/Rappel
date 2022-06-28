import { Grid } from "@nextui-org/react";
import React, { useContext } from "react";
import useSWR from "swr";
import { AuthContextInterface, AuthContext } from "../../Contexts/Auth";
import { getUserTodoSubtasksListQuery } from "../../services/Queries";
import { UserTodoSubtasksListURL } from "../../services/QueriesGateway";
import { ProgressBar } from "../../types";
import { ShowFilterType } from "../FilterBar";
import SubtaskItem from "../SubtaskItem";

export interface FilteredSubtaskdataProps {
  id: number;
  currentShowing: ShowFilterType;
  onFilterChange: (value?: ShowFilterType) => void;
}

const Index = ({
  id,
  currentShowing,
  onFilterChange,
}: FilteredSubtaskdataProps): JSX.Element => {
  const { token } = useContext<AuthContextInterface>(AuthContext);
  const { data } = useSWR(UserTodoSubtasksListURL(id), () =>
    getUserTodoSubtasksListQuery(token + "", id)
  );

  if (!data) return <>loading...</>;

  switch (currentShowing) {
    case ShowFilterType.ALL:
      return (
        <>
          {data.map((td) => (
            <Grid xs={12} key={td.id} css={{ padding: "$0" }}>
              <SubtaskItem
                id={td.id}
                parentId={id}
                notifyChange={onFilterChange}
              />
            </Grid>
          ))}
        </>
      );

    case ShowFilterType.COMPLETED:
      return (
        <>
          {data
            .filter((td) => td.status === ProgressBar.COMPLETED)
            .map((td) => (
              <Grid xs={12} key={td.id} css={{ padding: "$0" }}>
                <SubtaskItem
                  id={td.id}
                  parentId={id}
                  notifyChange={onFilterChange}
                />
              </Grid>
            ))}
        </>
      );
    case ShowFilterType.STARTED:
      return (
        <>
          {data
            .filter((td) => td.status === ProgressBar.STARTED)
            .map((td) => (
              <Grid xs={12} key={td.id} css={{ padding: "$0" }}>
                <SubtaskItem
                  id={td.id}
                  parentId={id}
                  notifyChange={onFilterChange}
                />
              </Grid>
            ))}
        </>
      );
    default:
      return <></>;
  }
};

export default Index;
