import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { AppUser } from '../_models/app-user';
import { PaginatedResult } from '../_models/pagination';
import { RegisterModel } from '../_models/register-model';
import { UserChangePass } from '../_models/user-change-pass';
import { UserUpdateInformation } from '../_models/user-update-information';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }
  register(model: RegisterModel) {
    return this.http.post<any>(this.baseUrl + 'account/register', model, {});
  }
  getAll(page?, itemsPerPage?): Observable<PaginatedResult<any[]>> {
    const paginatedResult: PaginatedResult<any[]> = new PaginatedResult<any[]>();
    let params = new HttpParams();
    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }
    return this.http.get<any[]>(this.baseUrl + 'account/getAll', { observe: 'response', params })
      .pipe(
        map(response => {
          console.log(response);
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        }),
      );
  }
  getInformationUser(userName: string) {
    return this.http.get<any>(this.baseUrl + 'account/getInformationUser', {params: {userName: userName}});
  }
  removeUser(userName: string) {
    return this.http.delete(this.baseUrl + 'account/removeUser', {params: {userName: userName}});
  }
  changePass(param: UserChangePass) {
    return this.http.post(this.baseUrl + 'account/changePass', param, {});
  }
  updateInformationUser(param: UserUpdateInformation) {
    return this.http.post<boolean>(this.baseUrl + 'account/updateInformationUser', param, {});
  }
  updateUserRole(role: string, userName: string) {
    return this.http.get(this.baseUrl + 'rolesUser/UpdateUserRole', {params: {role:role, userName: userName}});
  }
}
