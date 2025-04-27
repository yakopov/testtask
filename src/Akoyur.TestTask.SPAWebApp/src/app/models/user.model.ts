export interface CreateUserRequest {
  email: string;
  password: string;
  provinceId: number;
}
export interface CreateUserResponse {
  userId: number;
}
export interface CheckEmailResponse {
  canBeRegistered: boolean;
}
