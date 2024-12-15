import { Component } from '@angular/core';
import {Router, RouterLink} from '@angular/router';

@Component({
  selector: 'app-server-error',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './server-error.component.html',
  styleUrl: './server-error.component.css'
})
export class ServerErrorComponent {
  error: any;

  constructor(private routerSrv: Router) {
    const currentNavigation = routerSrv.getCurrentNavigation();
    this.error = currentNavigation?.extras?.state?.['error'];
  }
}
