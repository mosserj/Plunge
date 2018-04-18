import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap } from 'rxjs/operators/tap';

import { AppUserAuth } from './app-user-auth';
import { AppUser } from './app-user';

const API_URL = "http://localhost:5000/api/security/";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable()
export class SecurityService {
  securityObject: AppUserAuth = new AppUserAuth();

  constructor(private http: HttpClient) { }

  login(entity: AppUser): Observable<AppUserAuth> {
    // Initialize security object
    this.resetSecurityObject();

    return this.http.post<AppUserAuth>(API_URL + "login",
      entity, httpOptions).pipe(
        tap(resp => {
          // Use object assign to update the current object
          // NOTE: Don't create a new AppUserAuth object
          //       because that destroys all references to object
          Object.assign(this.securityObject, resp);
          // Store into local storage
          localStorage.setItem("bearerToken",
            this.securityObject.bearerToken);
        }));
  }

  logout(): void {
    this.resetSecurityObject();
  }

  resetSecurityObject(): void {
    this.securityObject.userName = "";
    this.securityObject.bearerToken = "";
    this.securityObject.isAuthenticated = false;

    this.securityObject.roles = [];

    localStorage.removeItem("bearerToken");
  }

  // This method can be called a couple of different ways
  // *hasRole="'roleType'"  // Assumes roleValue is true
  // *hasRole="'roleType:value'"  // Compares roleValue to value
  // *hasRole="['roleType1','roleType2:value','roleType3']"
  hasRole(roleType: any, roleValue?: any) {
    let ret: boolean = false;

    // See if an array of values was passed in.
    if (typeof roleType === "string") {
      ret = this.isRoleValid(roleType, roleValue);
    }
    else {
      let roles: string[] = roleType;
      if (roles) {
        for (let index = 0; index < roles.length; index++) {
          ret = this.isRoleValid(roles[index]);
          // If one is successful, then let them in
          if (ret) {
            break;
          }
        }
      }
    }

    return ret;
  }


  private isRoleValid(roleType: string, roleValue?: string): boolean {
    let ret: boolean = false;
    let auth: AppUserAuth = null;

    // Retrieve security object
    auth = this.securityObject;
    if (auth) {
      // See if the claim type has a value
      // *hasRole="'roleType:value'"
      if (roleType.indexOf(":") >= 0) {
        let words: string[] = roleType.split(":");
        roleType = words[0].toLowerCase();
        roleValue = words[1];
      }
      else {
        roleType = roleType.toLowerCase();
        // Either get the claim value, or assume 'true'
        roleValue = roleValue ? roleValue : "true";
      }
      // Attempt to find the claim
      ret = auth.roles.find(c =>
        c.roleType.toLowerCase() == roleType &&
        c.roleValue == roleValue) != null;
    }

    return ret;
  }
}