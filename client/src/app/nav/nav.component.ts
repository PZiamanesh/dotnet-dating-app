import {Component, inject} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {AccountService} from '../_services/account.service';
import {BsDropdownModule} from 'ngx-bootstrap/dropdown';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {Router, RouterLink, RouterLinkActive} from '@angular/router';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [
    FormsModule,
    BsDropdownModule,
    RouterLinkActive,
    RouterLink
  ],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  accountSrv = inject(AccountService);
  private routerSrv = inject(Router);
  private toasterSrv = inject(ToastrService);

  loginDto: any = {};

  login() {
    this.accountSrv.login(this.loginDto).subscribe({
      next: (result) => {
        this.routerSrv.navigateByUrl('/members');
      },
      error: (error) => {
        // this.toasterSrv.error(error.error);
        console.log(error);
      }
    });
  }

  logout() {
    this.accountSrv.logout();
    this.routerSrv.navigateByUrl('/');
  }

}
