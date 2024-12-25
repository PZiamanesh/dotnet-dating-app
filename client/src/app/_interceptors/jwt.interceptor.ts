import {HttpInterceptorFn} from '@angular/common/http';
import {inject} from '@angular/core';
import {AccountService} from '../_services/account.service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const accountSrv = inject(AccountService);

  if (accountSrv.currentUser()) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${accountSrv.currentUser()?.token}`
      }
    })
  }

  return next(req);
};
