import { TodoResponseDto, UserResponse } from "../types";
import { api } from "../utils/apiAxiosInstance";

export const getCurrentUserQuery = (token: string) =>
  api(token).get<UserResponse>(`/auth/user`);

export const getUserTodoListQuery = (token: string) =>
  api(token).get<TodoResponseDto[]>(`/todos`);
