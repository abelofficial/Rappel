import { Container } from "@nextui-org/react";
import type { NextPage } from "next";
import { useContext } from "react";
import AddTodoFormModal from "../components/AddTodoFormModal";
import { AuthContext, AuthContextInterface } from "../Contexts/Auth";

const Home: NextPage = () => {
  const { user, setUser } = useContext<AuthContextInterface>(AuthContext);

  console.log("User: ", user);
  return (
    <Container>
      <AddTodoFormModal />
    </Container>
  );
};

export default Home;
