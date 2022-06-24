import { SubtaskResponseDto, TodoResponseDto, UserResponse } from "../types";
import { api } from "../utils/apiAxiosInstance";

export const getCurrentUserQuery = (token: string) =>
  api(token).get<UserResponse>(`/auth/user`);

export const getUserTodoListQuery = (token: string) =>
  api(token).get<TodoResponseDto[]>(`/todos`);

export const getUserTodoSubtasksListQuery = (token: string, id: number) =>
  api(token).get<SubtaskResponseDto[]>(`/todo/${id}/todossubtasks`);
