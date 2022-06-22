import { Container } from "@nextui-org/react";
import type { NextPage } from "next";
import RegisterForm from "../components/RegisterForm";

const Home: NextPage = () => {
  return (
    <Container>
      <RegisterForm />
    </Container>
  );
};

export default Home;
