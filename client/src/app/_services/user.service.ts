import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: "root"
})
export class UserService {
  httpSrv = inject(HttpClient);
  baseUrl = 'https://localhost:5001/api/';

  getUsers() {
    return this.httpSrv.get(this.baseUrl + 'users');
  }
}
