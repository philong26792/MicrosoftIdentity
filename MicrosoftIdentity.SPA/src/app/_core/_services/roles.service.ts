import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { AppRoles } from '../_models/app-role';
@Injectable({
  providedIn: 'root'
})
export class RolesService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }
  getAll() {
    return this.http.get<AppRoles[]>(this.baseUrl + 'settingRoles/getAll', {});
  }
  create(data: AppRoles) {
    return this.http.post<any>(this.baseUrl + 'settingRoles/create',data);
  }
  delete(roleName: string) {
    return this.http.delete(this.baseUrl + 'settingRoles/delete', {params: {roleName: roleName}});
  }
  update(data: AppRoles) {
    return this.http.put(this.baseUrl + 'settingRoles/update', data);
  }
}
