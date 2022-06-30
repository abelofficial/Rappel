import { Grid } from "@nextui-org/react";
import type { NextPage } from "next";
import { Tab, TabList, TabPanel, Tabs } from "react-tabs";
import LoginForm from "../components/LoginForm";
import RegisterForm from "../components/RegisterForm";

const Auth: NextPage = () => {
  return (
    <Grid.Container
      alignItems='center'
      css={{
        h: "100vh",
        backgroundColor: "transparent",
        backgroundImage: "/blueprints.svg",
        backgroundRepeat: "no-repeat, repeat",
        backgroundSize: "cover",
      }}
    >
      <Tabs>
        <TabList style={{ display: "flex", justifyContent: "space-around" }}>
          <Tab>Login</Tab>
          <Tab>Register</Tab>
        </TabList>
        <TabPanel>
          <LoginForm />
        </TabPanel>
        <TabPanel>
          <RegisterForm />
        </TabPanel>
      </Tabs>
    </Grid.Container>
  );
};

export default Auth;
