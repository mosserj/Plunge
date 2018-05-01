import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap } from 'rxjs/operators/tap';
import { environment } from "../../environments/environment";
import { md5 } from "./md5";
import 'rxjs/add/operator/map';
import { JwtHelper } from './angular2-jwt';

import { AppUserAuth } from './app-user-auth';
import { AppUser } from './app-user';
import { HttpResponse } from 'selenium-webdriver/http';

const API_URL = environment.devHostJavaEnabled ? environment.devHostJava : environment.devHostCORE;
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable()
export class SecurityService {
  securityObject: AppUserAuth = new AppUserAuth();
  jwt: JwtHelper;

  constructor(private http: HttpClient) {
    this.jwt = new JwtHelper();
   }

  isAuthenticated() {
    if (!localStorage.getItem("user") || localStorage.getItem("user") == "") {
      return false;
    } else {
      return true;
    }
  }

  login(entity: AppUser): Observable<AppUserAuth> {
    // Initialize security object
    this.resetSecurityObject();

    entity.password = md5(entity.password);

    return this.http.post<AppUserAuth>(API_URL + "/api/login",
      entity, httpOptions).pipe(
        tap(resp => {
          Object.assign(this.securityObject, resp);
          // Store into local storage
          let decodedToken = this.jwt.decodeToken(this.securityObject.bearerToken);
          localStorage.setItem("bearerToken", decodedToken.Token);
        }));
  }


  register(entity: AppUser): Observable<any> {
    // Initialize security object
    this.resetSecurityObject();

    entity.password = md5(entity.password);

    return this.http.post<HttpResponse>(API_URL + "/api/signup",
      entity, httpOptions)
      .map((response: Response) => response['data'])
      .pipe(
        tap(resp => {
          Object.assign(this.securityObject, resp);
          let decodedToken = this.jwt.decodeToken(this.securityObject.bearerToken);
          // Store into local storage
          localStorage.setItem("userName", this.securityObject.userName);
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