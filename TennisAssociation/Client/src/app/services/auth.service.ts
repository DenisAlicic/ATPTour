import { ChangePassModel, UserModel } from '../models/user.model';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly authUrl = 'http://localhost:8080/api/account/';
  
  private readonly loginUrl = 'login';
  private readonly registerUrl = 'register';
  private readonly changePassUrl = 'changepass';

  constructor(private http: HttpClient) {}

  signup(formData) {
    const body = { ...formData};
    return this.http.post<UserModel>(this.authUrl + this.registerUrl, body);
  }

  login(data) {
    const body = {
      ...data
    };

    return this.http.post<any>(this.authUrl + this.loginUrl, body)
      .pipe(map(user => {
        // login successful if there's a jwt token in the response
        if (user) { //TODO: && user.token
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify(user));
        }

        return user;
      }));
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
  }

  changePassword(formData) {
    const body = { ...formData, username: this.username };
    return this.http.post(this.authUrl + this.changePassUrl, body);
  }

  isLogged() {
    return localStorage.getItem("currentUser") !== null;
  }

  isAdmin() {
    return JSON.parse(localStorage.getItem('currentUser')).isAdmin;
  }

  get username() {
    return JSON.parse(localStorage.getItem('currentUser')).userName;
  }
}
