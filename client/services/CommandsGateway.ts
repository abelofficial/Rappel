export const RegisterUserURL = () => `/auth/register`;

export const LoginUserURL = () => `/auth/login`;

export const CreateTodoURL = (id: number) => `project/${id}/todos`;

export const CreateSubtaskURL = (id: number) => `/todo/${id}/todossubtasks`;

export const UpdateSubtaskStatusURL = (id: number, subTaskId: number) =>
  `/todo/${id}/todossubtasks/${subTaskId}`;

export const CreateProjectURL = () => `/projects`;
