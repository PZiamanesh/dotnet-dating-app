import {HttpInterceptorFn} from '@angular/common/http';
import {catchError} from 'rxjs';
import {inject} from '@angular/core';
import {NavigationExtras, Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const routerSrv = inject(Router);
  const toastrSrv = inject(ToastrService);

  return next(req).pipe(
    catchError(error => {
      if (error) {
        switch (error.status) {
          case 400:
            const errorList = error.error.errors;
            if (errorList) {
              const flatErrorList = [];
              for (let key in errorList) {
                if (errorList[key]) {
                  flatErrorList.push(errorList[key]);
                }
              }
              throw flatErrorList.flat();
            } else {
              toastrSrv.error(error.error, error.status);
            }
            break;
          case 401:
            toastrSrv.error(error.error, error.status);
            break;
          case 404:
            routerSrv.navigateByUrl('/not-found');
            break;
          case 500:
            const navigationExtras: NavigationExtras = {state: {error: error.error}};
            routerSrv.navigateByUrl('/server-error', navigationExtras);
            break;
          default:
            toastrSrv.error('an unexpected error has occurred!');
            break;
        }
      }
      throw error;
    })
  );
};
