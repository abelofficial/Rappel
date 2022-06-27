import { Container, Grid } from "@nextui-org/react";
import React from "react";
import Header from "../Header";

const Index = (props: { children: JSX.Element }) => {
  return (
    <div style={{ position: "relative", width: "100vw", maxWidth: "100%" }}>
      <Header />
      <Container
        justify='space-between'
        gap={1}
        css={{
          maxWidth: "100%",
          minHeight: "100vh",
          paddingTop: "4.5rem",
          position: "absolute",
          zIndex: 400,
        }}
      >
        <Grid xs={12} sm={8}>
          {props.children}
        </Grid>
      </Container>
    </div>
  );
};

export default Index;
