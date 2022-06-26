import { Grid, Spacer, Text } from "@nextui-org/react";
import Image from "next/image";
import React from "react";

const Index = () => {
  return (
    <Grid.Container
      alignItems='center'
      justify='flex-start'
      css={{
        position: "fixed",
        left: 0,
        zIndex: 600,
        height: "4rem",
        boxShadow: "$sm",
        padding: "$9",
        shadow: "$sm",
        backgroundColor: "$background",
      }}
    >
      <Image src='/note-img.svg' alt='start-icon' width={30} height={30} />
      <Spacer />
      <Text h4> Todo</Text>
    </Grid.Container>
  );
};

export default Index;
