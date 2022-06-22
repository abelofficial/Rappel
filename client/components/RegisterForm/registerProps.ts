import { RegisterUserRequest } from "../../types";
import { string, ref as yupRef, SchemaOf, object } from "yup";

export const fieldLabels: RegisterUserRequest = {
  firstName: "First name",
  lastName: "Last name",
  email: "Email address",
  username: "Username",
  password: "password",
  confirmPassword: "Confirm password",
};

export const fieldNames: RegisterUserRequest = {
  firstName: "firstName",
  lastName: "lastName",
  email: "email",
  username: "username",
  password: "password",
  confirmPassword: "confirmPassword",
};

export const initialValues: RegisterUserRequest = {
  firstName: "",
  lastName: "",
  email: "",
  username: "",
  password: "",
  confirmPassword: "",
};

export const validationSchema: SchemaOf<RegisterUserRequest> = object({
  firstName: string().required("First name is required"),
  lastName: string().required("Last name is required"),
  email: string()
    .required("Email address is required")
    .email("Please provide a valid email address"),
  username: string().required("Username is required"),
  password: string()
    .required("Password is required")
    .min(6, "Password must be min 6 characters."),
  confirmPassword: string()
    .required("Confirm you password")
    .oneOf([yupRef(`${[fieldNames.password]}`), null], "Passwords must match"),
});
