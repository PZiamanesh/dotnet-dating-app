import {CanActivateFn} from '@angular/router';
import {inject} from '@angular/core';
import {AccountService} from '../_services/account.service';
import {ToastrService} from 'ngx-toastr';

export const authGuard: CanActivateFn = (route, state) => {
  const accountSrv = inject(AccountService);
  const toastrSrv = inject(ToastrService);

  if (accountSrv.currentUser()) {
    return true;
  } else {
    toastrSrv.error('You shall not pass!');
    return false;
  }
};
