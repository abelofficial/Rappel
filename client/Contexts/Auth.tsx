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

    if (shouldUserAuth()) {
      redirectToAuth();
    }
  }, []);

  const shouldUserAuth = () => !auth.user && !router.pathname.endsWith("/auth");

  if (shouldUserAuth()) return <h1> loading...</h1>;

  return <AuthContext.Provider value={auth}>{children}</AuthContext.Provider>;
};

function AuthActions(): AuthContextInterface {
  const [user, setUser] = useState<UserResponse | undefined>();

  return { user, setUser };
}
