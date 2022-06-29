export const CurrentUserURLUrl = () => `/auth/user`;

export const UserTodoItemURL = (id: number, projectId: number) =>
  `project/${projectId}/todos/${id}`;

export const UserTodoListURL = (id: number) => `project/${id}/todos`;

export const UserTodoSubtasksListURL = (id: number) =>
  `/todo/${id}/todossubtasks`;

export const UserTodoSubtaskURL = (
  id: number,
  parentId: number,
  projectId: number
) => `/todo/${parentId}/todossubtasks/${id}?projectId=${projectId}`;

export const UserProjectsListURL = () => `/projects`;

export const UserProjectsURL = (id: number) => `/projects/${id}`;
