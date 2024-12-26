import {HttpInterceptorFn} from '@angular/common/http';
import {inject} from '@angular/core';
import {BusyService} from '../_services/busy.service';
import {delay, finalize} from 'rxjs';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const busySrv = inject(BusyService);

  busySrv.busy();

  return next(req).pipe(
    finalize(() => {
      busySrv.idle();
    })
  );
};
