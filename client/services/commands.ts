import {
  LoginUserRequest,
  LoginUserResponse,
  RegisterUserRequest,
  UserResponse,
} from "../types";
import { api } from "../utils/apiAxiosInstance";

export const registerUserCommand = (
  body: Omit<RegisterUserRequest, "confirmPassword">
) => api().post<UserResponse>(`/auth/register`, body);

export const loginUserCommand = (body: LoginUserRequest) =>
  api().post<LoginUserResponse>(`/auth/login`, body);
