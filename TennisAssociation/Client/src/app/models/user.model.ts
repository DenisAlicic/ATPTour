export interface UserModel {
  firstname: string;
  lastname: string;
  email: string;
  username: string;
  password: string;
}

export interface ChangePassModel {
  username: string;
  password: string;
  newPassword: string;
}