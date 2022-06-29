import {
  ProjectResponse,
  SubtaskResponseDto,
  TodoResponseDto,
  UserResponse,
} from "../types";
import { api } from "../utils/apiAxiosInstance";
import * as Gateway from "./QueriesGateway";

export const getCurrentUserQuery = (token: string) =>
  api(token)
    .get<UserResponse>(Gateway.CurrentUserURLUrl())
    .then((d) => d.data);

export const getUserTodoItemQuery = (
  token: string,
  id: number,
  projectId: number
) =>
  api(token)
    .get<TodoResponseDto>(Gateway.UserTodoItemURL(id, projectId))
    .then((d) => d.data);

export const getUserTodoListQuery = (token: string, id: number) =>
  api(token)
    .get<TodoResponseDto[]>(Gateway.UserTodoListURL(id))
    .then((d) => d.data);

export const getUserTodoSubtasksListQuery = async (token: string, id: number) =>
  await api(token)
    .get<SubtaskResponseDto[]>(Gateway.UserTodoSubtasksListURL(id))
    .then((d) => d.data);

export const getUserTodoSubtaskQuery = async (
  token: string,
  id: number,
  parentId: number,
  projectId: number
) =>
  await api(token)
    .get<SubtaskResponseDto>(
      Gateway.UserTodoSubtaskURL(id, parentId, projectId)
    )
    .then((d) => d.data);

export const getUserProjectsListQuery = async (token: string) =>
  await api(token)
    .get<ProjectResponse[]>(Gateway.UserProjectsListURL())
    .then((d) => d.data);

export const getUserProjectsQuery = async (token: string, id: number) =>
  await api(token)
    .get<ProjectResponse>(Gateway.UserProjectsURL(id))
    .then((d) => d.data);
