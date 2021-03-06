import { UserModel } from './../models/user.database.model';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from 'rxjs/operators';



@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly usersUrl = 'http://localhost:3000/users/';
  private readonly studentUsernamesUrl = 'http://localhost:3000/students/profile/';

  constructor(private http: HttpClient) { }

  signup(formData) {
    const body = { ...formData};
    console.log(body);
    return this.http.post<UserModel>(this.usersUrl, body);
  }

  login(data) {
    const body = {
      ...data
    };

    return this.http.post<any>(this.usersUrl + body.username, body)
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

  // getStudents() {
  //   return this.http.get<UserDatabase[]>(this.studentsUrl);
  // }

  // getStudentById(id: string) {
  //   return this.http.get<UserDatabase>(this.studentsUrl + id);
  // }

  // getStudentByUsername(username: string) {
  //   return this.http.get<UserDatabase>(this.studentUsernamesUrl + username);
  // }

  // getStudentExistanceByUsername(username: string) {
  //   return this.http.get<String>(this.studentUsernamesUrl + username);
  // }

  // deleteStudentById(id: string) {
  //   return this.http.delete(this.studentsUrl + id);
  // }

}
