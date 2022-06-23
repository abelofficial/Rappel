import { useRouter } from "next/router";
import {
  createContext,
  Dispatch,
  SetStateAction,
  useEffect,
  useState,
} from "react";
import { UserResponse } from "../types";

export interface AuthContextInterface {
  user: UserResponse | undefined;
  setUser: Dispatch<SetStateAction<UserResponse | undefined>>;
}

export const AuthContext = createContext<AuthContextInterface>(
  {} as AuthContextInterface
);

export const AuthProvider = ({ children }: { children: JSX.Element }) => {
  const auth = AuthActions();
  const router = useRouter();

  useEffect(() => {
    const redirectToAuth = async () => {
      await router.push("/auth");
    };

    const redirectToHome = async () => {
      if (router.pathname == "/auth") await router.push("/");
    };

    if (!auth.user) {
      const savedUserString = localStorage.getItem("user");

      if (savedUserString == null) redirectToAuth();

      const savedUser: UserResponse = JSON.parse(
        savedUserString ? savedUserString : ""
      );
      auth.setUser(savedUser);
      redirectToHome();
    }
  }, []);

  if (!auth.user) return <h1> loading...</h1>;

  return <AuthContext.Provider value={auth}>{children}</AuthContext.Provider>;
};

function AuthActions(): AuthContextInterface {
  const [user, setUser] = useState<UserResponse | undefined>();

  return { user, setUser };
}
