import {
  CreateSubtaskCommand,
  CreateTodoCommand,
  LoginUserRequest,
  LoginUserResponse,
  RegisterUserRequest,
  TodoResponseDto,
  UserResponse,
} from "../types";
import { api } from "../utils/apiAxiosInstance";

export const registerUserCommand = (
  body: Omit<RegisterUserRequest, "confirmPassword">
) => api().post<UserResponse>(`/auth/register`, body);

export const loginUserCommand = (body: LoginUserRequest) =>
  api().post<LoginUserResponse>(`/auth/login`, body);

export const createTodoCommand = (token: string, body: CreateTodoCommand) =>
  api(token).post<TodoResponseDto>(`/todos`, body);

export const createSubtaskCommand = (
  token: string,
  id: number,
  body: CreateSubtaskCommand
) => api(token).post<CreateSubtaskCommand>(`/todo/${id}/todossubtasks`, body);
