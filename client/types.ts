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

export interface CreateProjectRequestDto {
  title: string;
  description: string;
  isOrdered: boolean;
}

export interface UpdateProjectRequestDto {
  title: string;
  description: string;
}

export interface ProjectResponse {
  id: number;
  title: string;
  description: string;
  isOrdered: boolean;
  owner: UserResponse;
  members: UserResponse[];
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
  COMPLETED = 2,
  STARTED = 1,
  CREATED = 0,
}

export interface UpdateSubtaskStatusCommandDto {
  status: ProgressBar;
}
