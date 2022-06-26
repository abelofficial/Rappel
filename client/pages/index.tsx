import { Container, Grid } from "@nextui-org/react";
import type { NextPage } from "next";
import { useContext } from "react";
import { AuthContext, AuthContextInterface } from "../Contexts/Auth";
import AddProjectFormModal from "../components/AddProjectFormModal";
import { getUserProjectsListQuery } from "../services/Queries";
import useSWR from "swr";
import ProjectCard from "../components/ProjectCard";

const Home: NextPage = () => {
  const { user, token } = useContext<AuthContextInterface>(AuthContext);
  const { data } = useSWR(`/projects`, () =>
    getUserProjectsListQuery(token + "")
  );

  if (!data) return <div>loading...</div>;
  console.log("DATA: ", data);
  return (
    <Container>
      <Grid.Container
        gap={1}
        css={{
          width: "100vw",
          display: "flex",
          alignItem: "center",
          justifyContent: "center",
        }}
      >
        <AddProjectFormModal />
        <Grid.Container direction='column' gap={1} justify='center'>
          {data.map((p) => (
            <Grid key={p.id}>
              <ProjectCard id={p.id} />
            </Grid>
          ))}
        </Grid.Container>
      </Grid.Container>
    </Container>
  );
};

export default Home;
