import { Container, Grid } from "@nextui-org/react";
import type { NextPage } from "next";
import { useRouter } from "next/router";
import { useContext } from "react";

import useSWR from "swr";
import AddTodoFormModal from "../../components/AddTodoFormModal";
import TodoItem from "../../components/TodoItem";
import { AuthContextInterface, AuthContext } from "../../Contexts/Auth";
import { getUserTodoListQuery } from "../../services/Queries";

const Home: NextPage = () => {
  const router = useRouter();
  const { id } = router.query;
  var projectId: number = +(id + "");
  const { token } = useContext<AuthContextInterface>(AuthContext);

  const { data } = useSWR(`projects/${id}/todos`, () =>
    getUserTodoListQuery(token + "", projectId)
  );

  if (!data) return <div>loading...</div>;

  return (
    <Container>
      <AddTodoFormModal id={projectId} />
      <Grid.Container
        gap={1}
        css={{
          display: "flex",
          alignItem: "center",
          justifyContent: "space-between",
        }}
      >
        {data?.map((td) => (
          <Grid xs={12} md={5} key={td.id}>
            <TodoItem
              id={td.id}
              projectId={projectId}
              title={td.title}
              description={td.description}
              status={td.status}
            />
          </Grid>
        ))}
      </Grid.Container>
    </Container>
  );
};

export default Home;
