import {inject, Injectable} from '@angular/core';
import {NgxSpinnerService} from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  private spinnerSrv = inject(NgxSpinnerService);
  busyRequestCount = 0;

  busy() {
    this.busyRequestCount++;
    this.spinnerSrv.show(undefined, {
      type: 'ball-pulse-sync',
      bdColor: 'rgba(255,255,255,0)',
      color: '#333333'
    });
  }

  idle() {
    this.busyRequestCount--;
    if (this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
      this.spinnerSrv.hide();
    }
  }
}
