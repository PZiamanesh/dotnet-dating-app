import {Component, inject, input, output} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {AccountService} from '../_services/account.service';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountSrv = inject(AccountService);
  private toasterSrv = inject(ToastrService);
  private router = inject(Router);

  cancelRegister = output<boolean>();
  model: any = {};

  register() {
    this.accountSrv.register(this.model).subscribe({
      next: response => {
        this.router.navigateByUrl('/members');
      },
      error: error => {
        this.toasterSrv.error(error.error);
      }
    });

  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
