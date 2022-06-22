import { Button, Container } from "@nextui-org/react";
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
    <Container display='flex' justify='center'>
      <Button.Group color='gradient' ghost>
        <Button onPress={() => setCurrentTab(Tabs.LOGIN)}>
          {Tabs.LOGIN.toString()}
        </Button>
        <Button onPress={() => setCurrentTab(Tabs.REGISTER)}>
          {Tabs.REGISTER}
        </Button>
      </Button.Group>
      {currentTab === Tabs.LOGIN ? <LoginForm /> : <RegisterForm />}
    </Container>
  );
};

export default Auth;
