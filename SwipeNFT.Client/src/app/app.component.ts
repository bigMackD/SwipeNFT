import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from './authentication/services/authentication.service';
import { NotificationService } from './shared/services/notification.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass'],
})
export class AppComponent {
  title = 'SwipeNFT.Client';
  testProp$: Observable<string>;

  constructor(
    private notificationService: NotificationService,
    private authService: AuthenticationService,
    private router: Router
  ) {}

  isLoggedIn(): boolean {
    if (localStorage.getItem('token') != null) return true;
    else return false;
  }

  onLogout(): void {
    this.authService.logout();
    this.notificationService.success('Successfully logged out!');
    this.router.navigate(['login']);
  }
}
