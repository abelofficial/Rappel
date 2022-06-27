import { Grid, Row, Text } from "@nextui-org/react";
import type { NextPage } from "next";
import { useRouter } from "next/router";
import { useContext } from "react";
import Lottie from "react-lottie-player";

import useSWR, { mutate } from "swr";
import TodoFormModal from "../../components/TodoFormModal";
import TodoItem from "../../components/TodoItem";
import { AuthContextInterface, AuthContext } from "../../Contexts/Auth";
import { getUserTodoListQuery } from "../../services/Queries";
import { UserTodoListURL } from "../../services/QueriesGateway";
import { CreateTodoCommand, TodoResponseDto } from "../../types";
import * as notItemAnim from "../../utils/Anims/not-found.json";
import * as Gateway from "../../services/QueriesGateway";
import { createTodoCommand } from "../../services/commands";

const Home: NextPage = () => {
  const router = useRouter();
  const { id } = router.query;
  var projectId: number = +(id + "");
  const { token } = useContext<AuthContextInterface>(AuthContext);

  const { data } = useSWR(UserTodoListURL(projectId), () =>
    getUserTodoListQuery(token + "", projectId)
  );

  if (!data) return <div>loading...</div>;

  const onAddTodoHandler = async (values: CreateTodoCommand) => {
    mutate(
      Gateway.UserTodoListURL(projectId),
      async (data: TodoResponseDto[]) => {
        const newTodo = await createTodoCommand(token + "", projectId, values);
        return [...data, newTodo];
      }
    );
  };

  return (
    <Grid.Container direction='column'>
      <Grid>
        <TodoFormModal
          id={projectId}
          buttonTitle='Create'
          title='Create new todo'
          onSubmit={onAddTodoHandler}
        />
      </Grid>
      <Row justify='center'>
        {data.length > 0 ? (
          <Grid.Container alignItems='flex-end' gap={1}>
            {data.map((td) => (
              <Grid xs={12} key={td.id}>
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
        ) : (
          <Grid.Container
            direction='column'
            alignItems='center'
            gap={2}
            css={{ padding: "$20 $1" }}
          >
            <Text h4> Your todo list will show here</Text>
            <Lottie
              loop={false}
              play
              animationData={notItemAnim}
              style={{ width: "100%", height: "100%", maxWidth: "30rem" }}
            />
          </Grid.Container>
        )}
      </Row>
    </Grid.Container>
  );
};

export default Home;
