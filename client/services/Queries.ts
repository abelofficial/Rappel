import {
  ProjectResponse,
  SubtaskResponseDto,
  TodoResponseDto,
  UserResponse,
} from "../types";
import { api } from "../utils/apiAxiosInstance";

export const getCurrentUserQuery = (token: string) =>
  api(token)
    .get<UserResponse>(`/auth/user`)
    .then((d) => d.data);

export const getUserTodoItemQuery = (token: string, id: number) =>
  api(token)
    .get<TodoResponseDto>(`/todos/${id}`)
    .then((d) => d.data);

export const getUserTodoListQuery = (token: string) =>
  api(token)
    .get<TodoResponseDto[]>(`/todos`)
    .then((d) => d.data);

export const getUserTodoSubtasksListQuery = async (token: string, id: number) =>
  await api(token)
    .get<SubtaskResponseDto[]>(`/todo/${id}/todossubtasks`)
    .then((d) => d.data);

export const getUserTodoSubtaskQuery = async (
  token: string,
  id: number,
  parentId: number
) =>
  await api(token)
    .get<SubtaskResponseDto>(`/todo/${parentId}/todossubtasks/${id}`)
    .then((d) => d.data);

export const getUserProjectsListQuery = async (token: string) =>
  await api(token)
    .get<ProjectResponse[]>(`/projects`)
    .then((d) => d.data);

export const getUserProjectsQuery = async (token: string, id: number) =>
  await api(token)
    .get<ProjectResponse>(`/projects/${id}`)
    .then((d) => d.data);
