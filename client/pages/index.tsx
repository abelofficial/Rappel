import { Container, Grid } from "@nextui-org/react";
import type { NextPage } from "next";
import { useContext } from "react";
import AddTodoFormModal from "../components/AddTodoFormModal";
import TodoItem from "../components/TodoItem";
import { AuthContext, AuthContextInterface } from "../Contexts/Auth";
import { getUserTodoListQuery } from "../services/Queries";
import useSWR from "swr";

const Home: NextPage = () => {
  const { user, token } = useContext<AuthContextInterface>(AuthContext);
  const { data, error } = useSWR("/todos", () =>
    getUserTodoListQuery(token + "")
  );

  if (!data) return <div>loading...</div>;

  console.log("error: ", error);
  return (
    <Container>
      <AddTodoFormModal />
      <Grid.Container
        gap={1}
        css={{
          display: "flex",
          alignItem: "center",
          justifyContent: "space-between",
        }}
      >
        {data.data?.map((td) => (
          <Grid xs={12} md={5} key={td.id}>
            <TodoItem
              id={td.id}
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
