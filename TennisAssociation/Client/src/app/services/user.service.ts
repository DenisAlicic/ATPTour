import { UserModel } from '../models/user.model';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly usersUrl = 'http://localhost:8080/api/account/';

  constructor(private http: HttpClient) {}

  signup(formData) {
    const body = { ...formData};
    console.log(body);
    return this.http.post<UserModel>(this.usersUrl + "register", body);
  }

  login(data) {
    const body = {
      ...data
    };

    return this.http.post<any>(this.usersUrl + "login", body)
      .pipe(map(user => {
        // login successful if there's a jwt token in the response
        if (user && user.token) {
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
}
