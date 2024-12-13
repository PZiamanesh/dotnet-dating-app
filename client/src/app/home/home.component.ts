import {Component, inject, OnInit} from '@angular/core';
import {RegisterComponent} from '../register/register.component';
import {AccountService} from '../_services/account.service';
import {UserService} from '../_services/user.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    RegisterComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  userSrv = inject(UserService);

  registerMode = false;
  users: any;

  ngOnInit() {
    this.users = this.userSrv.getUsers().subscribe({
      next: (response) => {
        this.users = response;
      },
      error: (err) => {
        console.log('users errrr');
      }
    });
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegister($event: boolean) {
    this.registerMode = $event;
  }
}
