import { Grid, Row, Text } from "@nextui-org/react";
import type { NextPage } from "next";
import Lottie from "react-lottie-player";
import { useContext } from "react";
import { AuthContext, AuthContextInterface } from "../Contexts/Auth";
import AddProjectFormModal from "../components/AddProjectFormModal";
import { getUserProjectsListQuery } from "../services/Queries";
import useSWR from "swr";
import ProjectCard from "../components/ProjectCard";
import { UserProjectsListURL } from "../services/QueriesGateway";
import * as notItemAnim from "../utils/Anims/no-item-anim.json";

const Home: NextPage = () => {
  const { user, token } = useContext<AuthContextInterface>(AuthContext);
  const { data } = useSWR(UserProjectsListURL(), () =>
    getUserProjectsListQuery(token + "")
  );

  if (!data) return <div>loading...</div>;

  return (
    <Grid.Container
      direction='column'
      alignItems='center'
      justify='space-around'
    >
      <Grid>
        <AddProjectFormModal />
      </Grid>
      <Row justify='center'>
        {data.length > 0 ? (
          <Grid.Container alignItems='flex-end' gap={1}>
            {data.map((p) => (
              <Grid xs={12} key={p.id}>
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
