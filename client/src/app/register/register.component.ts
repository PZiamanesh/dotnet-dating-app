import {Component, inject, input, output} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {AccountService} from '../_services/account.service';
import {ToastrService} from 'ngx-toastr';

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

  cancelRegister = output<boolean>();
  model: any = {};

  register() {
    this.accountSrv.register(this.model).subscribe({
      next: response => {
        console.log(response);
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
