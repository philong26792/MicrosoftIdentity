import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from '../_models/user';
import { HttpClient } from '@angular/common/http';
import { UserLogin } from '../_models/user-login';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl;
  jwtHelper = new JwtHelperService();
  currentUser: User;
  decodedToken: any;
  constructor(private http: HttpClient, private router: Router) { }
    login(userlogin: UserLogin) {
      return this.http.post(this.baseUrl + 'auth/login/', userlogin).pipe(
        map((response: any) => {
          console.log(response);
          const user = response;
          if (user) {
            localStorage.setItem('token', user.token);
            localStorage.setItem('user', JSON.stringify(user.user));
            localStorage.setItem('roles', JSON.stringify(user.roles));
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
            this.currentUser = user.user;
          }
        }),
      );
    }

    loggedIn() {
      const token = localStorage.getItem('token');
      return !this.jwtHelper.isTokenExpired(token);
    }

    logout() {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      this.decodedToken = null;
      this.currentUser = null;
      this.router.navigate(['/login']);
    }
  }
