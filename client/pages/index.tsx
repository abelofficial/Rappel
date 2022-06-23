import { Container, Grid } from "@nextui-org/react";
import type { NextPage } from "next";
import { useContext, useEffect, useState } from "react";
import AddTodoFormModal from "../components/AddTodoFormModal";
import TodoItem from "../components/TodoItem";
import { AuthContext, AuthContextInterface } from "../Contexts/Auth";
import { getUserTodoListQuery } from "../services/Queries";
import { TodoResponseDto } from "../types";

const Home: NextPage = () => {
  const [todoItems, setTodoItems] = useState(
    [] as Omit<TodoResponseDto[], "user">
  );
  const { user, token } = useContext<AuthContextInterface>(AuthContext);

  useEffect(() => {
    const fetchUserTodoList = async () => {
      const resp = await getUserTodoListQuery(token + "");
      setTodoItems(resp.data);
    };
    fetchUserTodoList();
  }, []);

  console.log("User: ", todoItems);
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
        {todoItems?.map((td) => (
          <Grid xs={12} sm={5} md={3} key={td.id}>
            <TodoItem id={0} title={td.title} description={td.description} />
          </Grid>
        ))}
      </Grid.Container>
    </Container>
  );
};

export default Home;
