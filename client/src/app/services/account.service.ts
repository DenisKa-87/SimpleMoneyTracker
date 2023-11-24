import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { ReplaySubject, map } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();
  constructor(private http: HttpClient) { }


  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  signIn(model: any) {
    return this.http.post(this.baseUrl + 'account/signin', model).pipe(
      map((user: User) => {
        // if(user){
        //   this.setCurrentUser(user);
        // }
        return user;
      })
    )
  }

  logIn(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
         const user = response
         if(user){
           this.setCurrentUser(user);
         }
        return user;
      })
    )
  }
}
