import {Component, inject, input, output} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {AccountService} from '../_services/account.service';

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
  accountSrv = inject(AccountService);

  cancelRegister = output<boolean>();
  model: any = {};

  register() {
    this.accountSrv.register(this.model).subscribe({
      next: response => {
        console.log(response);
      },
      error: err => {
        console.log(err);
      }
    });

  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
