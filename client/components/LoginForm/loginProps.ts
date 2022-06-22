import { LoginUserRequest } from "../../types";
import { string, SchemaOf, object } from "yup";

export const fieldLabels: LoginUserRequest = {
  username: "Username",
  password: "password",
};

export const fieldNames: LoginUserRequest = {
  username: "username",
  password: "password",
};

export const initialValues: LoginUserRequest = {
  username: "",
  password: "",
};

export const validationSchema: SchemaOf<LoginUserRequest> = object({
  username: string().required("Username is required"),
  password: string()
    .required("Password is required")
    .min(6, "Password must be min 6 characters."),
});
