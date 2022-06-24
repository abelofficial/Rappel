export interface RegisterUserRequest {
  firstName: string;
  lastName: string;
  email: string;
  username: string;
  password: string;
  confirmPassword: string;
}

export interface LoginUserRequest {
  username: string;
  password: string;
}

export interface UserResponse {
  firstName: string;
  lastName: string;
  email: string;
  username: string;
}

export interface LoginUserResponse {
  token: string;
  createdAt: Date;
}

export interface CreateTodoCommand {
  title: string;
  description: string;
}

export interface TodoResponseDto {
  id: number;
  title: string;
  description: string;
  status: ProgressBar;
  user: UserResponse;
}

export interface SubtaskResponseDto {
  id: number;
  title: string;
  description: string;
  status: ProgressBar;
  todo: TodoResponseDto;
}

export interface CreateSubtaskCommand extends CreateTodoCommand {}

export enum ProgressBar {
  COMPLETED = 0,
  STARTED = 1,
  CREATED = 2,
}
