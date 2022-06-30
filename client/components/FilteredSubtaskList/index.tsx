import { Grid, Row } from "@nextui-org/react";
import React, { useContext } from "react";
import Lottie from "react-lottie-player";
import useSWR from "swr";
import { AuthContextInterface, AuthContext } from "../../Contexts/Auth";
import { getUserTodoSubtasksListQuery } from "../../services/Queries";
import { UserTodoSubtasksListURL } from "../../services/QueriesGateway";
import { ProgressBar } from "../../types";
import { ShowFilterType } from "../FilterBar";
import SubtaskItem from "../SubtaskItem";
import * as emptyBoxAnim from "../../utils/Anims/empty-box.json";
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

  const getCompletedList = () =>
    data.filter((st) => st.status === ProgressBar.COMPLETED);

  const getStartedList = () =>
    data.filter((st) => st.status === ProgressBar.STARTED);

  const getCreatedList = () =>
    data.filter((st) => st.status === ProgressBar.CREATED);

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
          {getCompletedList().length > 0 ? (
            getCompletedList().map((st) => (
              <Grid xs={12} key={st.id} css={{ padding: "$0" }}>
                <SubtaskItem
                  id={st.id}
                  parentId={id}
                  notifyChange={onFilterChange}
                  projectId={st.projectId}
                />
              </Grid>
            ))
          ) : (
            <Row justify='center'>
              <Lottie
                loop={false}
                play
                animationData={emptyBoxAnim}
                style={{ width: "100%", height: "100%", maxWidth: "10rem" }}
              />
            </Row>
          )}
        </>
      );
    case ShowFilterType.STARTED:
      return (
        <>
          {getStartedList().length > 0 ? (
            getStartedList().map((st) => (
              <Grid xs={12} key={st.id} css={{ padding: "$0" }}>
                <SubtaskItem
                  id={st.id}
                  parentId={id}
                  notifyChange={onFilterChange}
                  projectId={st.projectId}
                />
              </Grid>
            ))
          ) : (
            <Row justify='center'>
              <Lottie
                loop={false}
                play
                animationData={emptyBoxAnim}
                style={{ width: "100%", height: "100%", maxWidth: "10rem" }}
              />
            </Row>
          )}
        </>
      );
    case ShowFilterType.NOT_STARTED:
      return (
        <>
          {getCreatedList().length > 0 ? (
            getCreatedList().map((st) => (
              <Grid xs={12} key={st.id} css={{ padding: "$0" }}>
                <SubtaskItem
                  id={st.id}
                  parentId={id}
                  notifyChange={onFilterChange}
                  projectId={st.projectId}
                />
              </Grid>
            ))
          ) : (
            <Row justify='center'>
              <Lottie
                loop={false}
                play
                animationData={emptyBoxAnim}
                style={{ width: "100%", height: "100%", maxWidth: "10rem" }}
              />
            </Row>
          )}
        </>
      );
    default:
      return <></>;
  }
};

export default Index;
