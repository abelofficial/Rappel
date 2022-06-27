import { Button, Grid, Row, Spacer, Text, User } from "@nextui-org/react";
import Image from "next/image";
import { useRouter } from "next/router";
import React, { useContext } from "react";
import { AuthContextInterface, AuthContext } from "../../Contexts/Auth";

const Index = () => {
  const router = useRouter();
  const { user, logoutUser } = useContext<AuthContextInterface>(AuthContext);

  const onLogoutHandler = () => {
    logoutUser();
    router.push("/auth");
  };
  return (
    <Grid.Container
      alignItems='center'
      justify='space-between'
      css={{
        position: "fixed",
        left: 0,
        zIndex: 600,
        height: "4rem",
        boxShadow: "$sm",
        shadow: "$sm",
        backgroundColor: "$background",
        padding: "$1 $8",
      }}
    >
      {!router.pathname.startsWith("/project") ? (
        <Grid css={{ display: "flex" }}>
          <Image src='/note-img.svg' alt='start-icon' width={30} height={30} />
          <Spacer />
          <Text h4> Todo</Text>
        </Grid>
      ) : (
        <Image
          src='/back-icon.svg'
          alt='start-icon'
          width={30}
          height={30}
          onClick={() => router.push("/")}
        />
      )}
      <Grid>
        {user && (
          <Row>
            <User
              bordered
              color='success'
              src='https://i.pravatar.cc/150?u=a042581f4e29026704d'
              name={user.firstName}
            />
            <Button
              auto
              color='warning'
              rounded
              bordered
              onPress={onLogoutHandler}
            >
              Logout
            </Button>
          </Row>
        )}
      </Grid>
    </Grid.Container>
  );
};

export default Index;
