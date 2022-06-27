import { useRouter } from "next/router";
import {
  createContext,
  Dispatch,
  SetStateAction,
  useEffect,
  useState,
} from "react";
import { loginUserCommand, registerUserCommand } from "../services/commands";
import { getCurrentUserQuery } from "../services/Queries";
import { LoginUserRequest, RegisterUserRequest, UserResponse } from "../types";

export interface AuthContextInterface {
  user: UserResponse | undefined;
  token: string | undefined;
  logInErrors: string[];
  registerErrors: string[];
  setUser: Dispatch<SetStateAction<UserResponse | undefined>>;
  setToken: Dispatch<SetStateAction<string | undefined>>;

  loginUser: (values: LoginUserRequest) => Promise<boolean>;
  registerUser: (values: RegisterUserRequest) => Promise<boolean>;
}

export const AuthContext = createContext<AuthContextInterface>(
  {} as AuthContextInterface
);

export const AuthProvider = ({ children }: { children: JSX.Element }) => {
  const auth = AuthActions();
  const router = useRouter();

  useEffect(() => {
    if (router.pathname != "/auth") {
      const redirectToAuth = async () => {
        await router.push("/auth");
      };

      const redirectToHome = async () => {
        await router.push("/");
      };

      if (!auth.user) {
        const savedUserString = localStorage.getItem("user");
        console.log("savedUserString: ", savedUserString);

        if (savedUserString) {
          const savedUser: UserResponse = JSON.parse(savedUserString + "");
          const userToken = localStorage.getItem(savedUser.username);
          const token = ("" + userToken)?.replaceAll('"', "");
          auth.setUser(savedUser);
          auth.setToken(token);
          redirectToHome();
        }

        redirectToAuth();
      }
    }
  }, []);

  return <AuthContext.Provider value={auth}>{children}</AuthContext.Provider>;
};

function AuthActions(): AuthContextInterface {
  const [user, setUser] = useState<UserResponse | undefined>();
  const [token, setToken] = useState<string | undefined>();
  const [logInErrors, setLogInErrors] = useState<string[]>([]);
  const [registerErrors, setRegisterErrors] = useState<string[]>([]);

  const loginUser = async (values: LoginUserRequest): Promise<boolean> => {
    try {
      var result = await loginUserCommand(values);
      var userResp = await getCurrentUserQuery(result.token);
      setUser(userResp);
      setToken(result.token);

      localStorage.setItem("user", JSON.stringify(userResp));
      localStorage.setItem(userResp.username, JSON.stringify(result.token));
      return true;
    } catch (e: any) {
      console.log("ERROR: ", e);
      setLogInErrors(e?.response?.data.errors);
      return false;
    }
  };

  const registerUser = async (
    values: RegisterUserRequest
  ): Promise<boolean> => {
    try {
      await registerUserCommand(values);
      return await loginUser(values);
    } catch (e: any) {
      console.log("ERROR: ", e);

      if (typeof e?.response?.data.errors === "string")
        setRegisterErrors(e?.response?.data.errors);
      return false;
    }
  };

  return {
    user,
    token,
    setUser,
    setToken,
    loginUser,
    logInErrors,
    registerUser,
    registerErrors,
  };
}
