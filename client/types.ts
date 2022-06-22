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
