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
  user: UserResponse;
}
