import { AppUserRole } from "./app-user-role";

export class AppUserAuth {
  userName: string = "";
  bearerToken: string = "";
  isAuthenticated: boolean = false;
  roles: AppUserRole[] = [];
}
