import { Grid, Row, Text } from "@nextui-org/react";
import type { NextPage } from "next";
import { v4 as uuid } from "uuid";
import Lottie from "react-lottie-player";
import { useContext } from "react";
import { AuthContext, AuthContextInterface } from "../Contexts/Auth";
import ProjectFormModal from "../components/ProjectFormModal";
import { getUserProjectsListQuery } from "../services/Queries";
import useSWR, { mutate } from "swr";
import ProjectCard from "../components/ProjectCard";
import { UserProjectsListURL } from "../services/QueriesGateway";
import * as notItemAnim from "../utils/Anims/no-item-anim.json";
import { createProjectCommand } from "../services/commands";
import { CreateProjectRequestDto, ProjectResponse } from "../types";
import * as Gateway from "../services/QueriesGateway";

const Home: NextPage = () => {
  const { user, token } = useContext<AuthContextInterface>(AuthContext);
  const { data } = useSWR(UserProjectsListURL(), () =>
    getUserProjectsListQuery(token + "")
  );

  if (!data) return <div>loading...</div>;

  const onAddProjectHandler = async (values: CreateProjectRequestDto) => {
    mutate(Gateway.UserProjectsListURL(), async (data: ProjectResponse[]) => {
      const newProject = await createProjectCommand(token + "", values);
      return [...data, newProject];
    });
  };

  return (
    <Grid.Container
      direction='column'
      alignItems='center'
      justify='space-around'
    >
      <Grid>
        <ProjectFormModal
          buttonTitle='Create'
          onSubmit={onAddProjectHandler}
          title='Create new project'
        />
      </Grid>
      <Row justify='center'>
        {data.length > 0 ? (
          <Grid.Container alignItems='flex-end' gap={1}>
            {data.map((p) => (
              <Grid xs={12} key={uuid()}>
                <ProjectCard id={p.id} />
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
            <Text h4> You haven not created a project yet</Text>
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
