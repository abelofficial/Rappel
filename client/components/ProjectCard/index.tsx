import { Card, Text, Grid, Button } from "@nextui-org/react";
import Image from "next/image";
import { useRouter } from "next/router";
import React, { useContext } from "react";
import useSWR, { mutate } from "swr";

import { AuthContextInterface, AuthContext } from "../../Contexts/Auth";
import { updateProjectCommand } from "../../services/commands";
import { getUserProjectsQuery } from "../../services/Queries";
import { UserProjectsURL } from "../../services/QueriesGateway";
import { ProjectResponse, UpdateProjectRequestDto } from "../../types";
import ProjectFormModal from "../ProjectFormModal";
import * as Gateway from "../../services/QueriesGateway";
("../../types");

export interface ProjectCardProps {
  id: number;
}

const Index = ({ id }: ProjectCardProps) => {
  const router = useRouter();
  const { token } = useContext<AuthContextInterface>(AuthContext);

  const { data } = useSWR(UserProjectsURL(id), () =>
    getUserProjectsQuery(token + "", id)
  );

  if (!data) return <div>loading subtasks...</div>;

  const onUpdateProjectHandler = async (values: UpdateProjectRequestDto) => {
    const request: UpdateProjectRequestDto = {
      title: values.title,
      description: values.description,
    };

    try {
      mutate(Gateway.UserProjectsURL(id), async (_d: ProjectResponse) => {
        const updatedProject = await updateProjectCommand(
          token + "",
          id,
          request
        );
        return updatedProject;
      });
    } catch (e) {
      console.log("ERROR: ", e);
    }
  };

  return (
    <Card>
      <Card.Header
        css={{
          display: "flex",
          alignItem: "center",
          justifyContent: "space-between",
        }}
      >
        <Grid.Container justify='space-between'>
          <Grid>
            <Text b>{data.title}</Text>
          </Grid>
          <Grid>
            <Grid.Container alignItems='center' justify='flex-end' gap={1}>
              <Grid>
                <ProjectFormModal
                  onSubmit={onUpdateProjectHandler}
                  propsValues={data}
                  actionButton={
                    <Image
                      src='/edit-icon.svg'
                      alt='An SVG of an eye'
                      width={20}
                      height={20}
                    />
                  }
                  buttonTitle='Update'
                  title={"Update project"}
                />
              </Grid>
              <Grid>
                <Image
                  src='/settings-icon.svg'
                  alt='An SVG of an eye'
                  width={20}
                  height={20}
                />
              </Grid>
            </Grid.Container>
          </Grid>
        </Grid.Container>
      </Card.Header>
      <Card.Divider />
      <Card.Body>
        <Text>{data.description}</Text>
      </Card.Body>

      <Card.Divider />
      <Card.Footer
        css={{
          display: "flex",
          alignItem: "center",
          justifyContent: "space-evenly",
        }}
      >
        <Button
          size='sm'
          color='gradient'
          bordered
          rounded
          onPress={() => router.push("project/" + id)}
        >
          Open
        </Button>
      </Card.Footer>
    </Card>
  );
};

export default Index;
