import {
  CreateSubtaskCommand,
  CreateTodoCommand,
  LoginUserRequest,
  LoginUserResponse,
  RegisterUserRequest,
  TodoResponseDto,
  UpdateSubtaskStatusCommandDto,
  UserResponse,
} from "../types";
import { api } from "../utils/apiAxiosInstance";

export const registerUserCommand = (
  body: Omit<RegisterUserRequest, "confirmPassword">
) =>
  api()
    .post<UserResponse>(`/auth/register`, body)
    .then((d) => d.data);

export const loginUserCommand = (body: LoginUserRequest) =>
  api()
    .post<LoginUserResponse>(`/auth/login`, body)
    .then((d) => d.data);

export const createTodoCommand = (token: string, body: CreateTodoCommand) =>
  api(token)
    .post<TodoResponseDto>(`/todos`, body)
    .then((d) => d.data);

export const createSubtaskCommand = (
  token: string,
  id: number,
  body: CreateSubtaskCommand
) =>
  api(token)
    .post<CreateSubtaskCommand>(`/todo/${id}/todossubtasks`, body)
    .then((d) => d.data);

export const updateSubtaskStatusCommand = (
  token: string,
  id: number,
  subTaskId: number,
  body: UpdateSubtaskStatusCommandDto
) =>
  api(token)
    .patch<CreateSubtaskCommand>(`/todo/${id}/todossubtasks/${subTaskId}`, body)
    .then((d) => d.data);
