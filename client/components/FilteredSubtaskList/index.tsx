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
          {data.map((st) => (
            <Grid xs={12} key={st.id} css={{ padding: "$0" }}>
              <SubtaskItem
                id={st.id}
                parentId={id}
                notifyChange={onFilterChange}
                projectId={st.projectId}
              />
            </Grid>
          ))}
        </>
      );

    case ShowFilterType.COMPLETED:
      return (
        <>
          {data
            .filter((st) => st.status === ProgressBar.COMPLETED)
            .map((st) => (
              <Grid xs={12} key={st.id} css={{ padding: "$0" }}>
                <SubtaskItem
                  id={st.id}
                  parentId={id}
                  notifyChange={onFilterChange}
                  projectId={st.projectId}
                />
              </Grid>
            ))}
        </>
      );
    case ShowFilterType.STARTED:
      return (
        <>
          {data
            .filter((st) => st.status === ProgressBar.STARTED)
            .map((st) => (
              <Grid xs={12} key={st.id} css={{ padding: "$0" }}>
                <SubtaskItem
                  id={st.id}
                  parentId={id}
                  notifyChange={onFilterChange}
                  projectId={st.projectId}
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
