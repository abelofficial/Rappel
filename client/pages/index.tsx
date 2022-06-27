import { Grid, Row } from "@nextui-org/react";
import type { NextPage } from "next";
import { useContext } from "react";
import { AuthContext, AuthContextInterface } from "../Contexts/Auth";
import AddProjectFormModal from "../components/AddProjectFormModal";
import { getUserProjectsListQuery } from "../services/Queries";
import useSWR from "swr";
import ProjectCard from "../components/ProjectCard";
import { UserProjectsListURL } from "../services/QueriesGateway";

const Home: NextPage = () => {
  const { user, token } = useContext<AuthContextInterface>(AuthContext);
  const { data } = useSWR(UserProjectsListURL(), () =>
    getUserProjectsListQuery(token + "")
  );

  if (!data) return <div>loading...</div>;

  return (
    <Grid.Container direction='column' alignItems='flex-end'>
      <Grid>
        <AddProjectFormModal />
      </Grid>
      <Row>
        <Grid.Container alignItems='flex-end' gap={1}>
          {data.map((p) => (
            <Grid xs={12} key={p.id}>
              <ProjectCard id={p.id} />
            </Grid>
          ))}
        </Grid.Container>
      </Row>
    </Grid.Container>
  );
};

export default Home;
