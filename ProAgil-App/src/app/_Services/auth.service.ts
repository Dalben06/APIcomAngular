import { Injectable } from '@angular/core';
import { JwtHelperService } from "@auth0/angular-jwt";
import { HttpClient } from '@angular/common/http';
import { map } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseURL = 'https://localhost:44376/api/user/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
constructor(private http: HttpClient) { }

login(model: any){
  return this.http.post(`${this.baseURL}login`, model).pipe(
    map((reponse: any) => {
      const user = reponse;
      if (user) {
        localStorage.setItem( 'Token', user.token );
        this.decodedToken = this.jwtHelper.decodeToken(user.token);
        sessionStorage.setItem('username', this.decodedToken.unique_name);
      }
    })
  )
}

register(model: any) {
  return this.http.post(`${this.baseURL}Register`, model);
}

loggedIn() {
  const token = localStorage.getItem('Token');
  return !this.jwtHelper.isTokenExpired(token);
}
}
