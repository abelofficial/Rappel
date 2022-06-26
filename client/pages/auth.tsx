import { Button, Grid } from "@nextui-org/react";
import type { NextPage } from "next";
import { useState } from "react";
import LoginForm from "../components/LoginForm";
import RegisterForm from "../components/RegisterForm";

const Auth: NextPage = () => {
  enum Tabs {
    REGISTER = "Register",
    LOGIN = "Login",
  }
  const [currentTab, setCurrentTab] = useState<Tabs>(Tabs.LOGIN);

  return (
    <Grid.Container
      direction='column'
      alignItems='center'
      justify='center'
      gap={3}
    >
      <Grid>
        <Button.Group color='gradient' ghost>
          <Button onPress={() => setCurrentTab(Tabs.LOGIN)}>
            {Tabs.LOGIN.toString()}
          </Button>
          <Button onPress={() => setCurrentTab(Tabs.REGISTER)}>
            {Tabs.REGISTER}
          </Button>
        </Button.Group>
      </Grid>
      <Grid>
        {currentTab === Tabs.LOGIN ? <LoginForm /> : <RegisterForm />}
      </Grid>
    </Grid.Container>
  );
};

export default Auth;
