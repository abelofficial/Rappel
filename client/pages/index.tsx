import { Container } from "@nextui-org/react";
import type { NextPage } from "next";
import LoginForm from "../components/LoginForm";
import RegisterForm from "../components/RegisterForm";

const Home: NextPage = () => {
  return (
    <Container>
      <RegisterForm />
      <LoginForm />
    </Container>
  );
};

export default Home;
