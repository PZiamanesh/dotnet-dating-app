import {inject, Injectable, signal} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Member} from '../_models/member';
import {Photo} from '../_models/photo';
import {PaginatedResult} from '../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class MemberService {
  private httpSrv = inject(HttpClient);
  baseUrl = environment.apiUrl;
  paginatedResult = signal<PaginatedResult<Member[]> | null>(null);

  getMembers(pageNumber?: number, pageSize?: number) {
    let queryString = new HttpParams();

    if (pageNumber && pageSize) {
      queryString = queryString.append('pageNumber', pageNumber);
      queryString = queryString.append('pageSize', pageSize);
    }

    return this.httpSrv.get<Member[]>(this.baseUrl + 'users', {observe: 'response', params: queryString}).subscribe({
      next: (response) => {
        this.paginatedResult.set({
          items: response.body as Member[],
          pagination: JSON.parse(response.headers.get("Pagination")!)
        });
      }
    });
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
