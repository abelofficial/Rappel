import { Card, Text, Grid, Button, Avatar } from "@nextui-org/react";
import Image from "next/image";
import { useRouter } from "next/router";
import React, { useContext } from "react";
import useSWR from "swr";

import { AuthContextInterface, AuthContext } from "../../Contexts/Auth";
import { getUserProjectsQuery } from "../../services/Queries";
import { UserProjectsURL } from "../../services/QueriesGateway";
("../../types");

export interface ProjectCardProps {
  id: number;
}

const Index = ({ id }: ProjectCardProps) => {
  const router = useRouter();
  const { user, token } = useContext<AuthContextInterface>(AuthContext);

  const { data } = useSWR(UserProjectsURL(id), () =>
    getUserProjectsQuery(token + "", id)
  );

  if (!data) return <div>loading subtasks...</div>;

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
            <Grid.Container justify='flex-end' gap={1}>
              <Grid>
                <Image
                  src='/edit-icon.svg'
                  alt='An SVG of an eye'
                  width={20}
                  height={20}
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

        {data.members.length > 1 && (
          <Grid.Container direction='column' alignContent='center' gap={1}>
            <Grid>
              <Avatar.Group count={data.members.length}>
                {data.members.map((m, i) => (
                  <Avatar
                    key={i}
                    size='sm'
                    pointer
                    text={
                      user?.username === m.username
                        ? "Me"
                        : m.username.charAt(0)
                    }
                    stacked
                  />
                ))}
              </Avatar.Group>
            </Grid>
            <Grid>
              <Text h4>Members</Text>
            </Grid>
          </Grid.Container>
        )}
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
