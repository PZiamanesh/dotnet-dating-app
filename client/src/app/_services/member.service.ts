import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Member} from '../_models/member';
import {Photo} from '../_models/photo';

@Injectable({
  providedIn: 'root'
})
export class MemberService {
  private httpSrv = inject(HttpClient);
  baseUrl = environment.apiUrl;

  getMembers() {
    return this.httpSrv.get<Member[]>(this.baseUrl + 'users');
  }

  getMember(username: string) {
    return this.httpSrv.get<Member>(this.baseUrl + `users/${username}`);
  }

  updateMember(member: Member) {
    return this.httpSrv.put(this.baseUrl + `users`, member);
  }

  setMainPhoto(photo: Photo) {
    return this.httpSrv.put(this.baseUrl + `users/set-main-photo/` + photo.id, {});
  }

  deletePhoto(photoId: number) {
    return this.httpSrv.delete(this.baseUrl + 'users/delete-photo/' + photoId);
  }

}
