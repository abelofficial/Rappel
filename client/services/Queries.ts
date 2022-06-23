import { UserResponse } from "../types";
import { api } from "../utils/apiAxiosInstance";

export const getCurrentUserQuery = (token: string) =>
  api(token).get<UserResponse>(`/auth/user`);
