import {Component, inject, OnInit} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {NavComponent} from './nav/nav.component';
import {AccountService} from './_services/account.service';
import {HomeComponent} from './home/home.component';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    NavComponent,
    HomeComponent,
    FormsModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  accountService = inject(AccountService);

  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser() {
    let user = localStorage.getItem('user');
    if (!user) return;
    // this.accountService.currentUser.set(JSON.parse(user));
    this.accountService.currentUser = JSON.parse(user);
  }
}
