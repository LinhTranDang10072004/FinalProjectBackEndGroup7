export interface LoginRequest {
  emailOrPhone: string;
  password: string;
}

export interface LoginResponse {
  success: boolean;
  token?: string;
  user?: UserInfo;
  message?: string;
}

export interface UserInfo {
  userID: number;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  userRole: string; // "Admin", "Receptionist", "Customer"
}


