import { AuthenticationService } from "src/app/authentication/services/authentication.service";
import { NotificationService } from "../services/notification.service";

import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable, tap } from "rxjs";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  errorMsg = 'Something went wrong, please contact administrator!';
  constructor(private auth: AuthenticationService,
     private router:Router, 
     private notificationService:NotificationService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    console.log('intercept', request)
    const token = localStorage.getItem('token');
    if(token){
        console.log('weszlo w token')
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
      return next.handle(request)
      .pipe(
        tap(
          () => {},
          err =>{
            if(err.status = 401){
              this.router.navigateByUrl('/login');
              this.auth.logout();
              this.notificationService.error('You have been logged out due to inactivity!') 
            }
            else{
              this.notificationService.error(this.errorMsg) 
            }
          }
        )
      );
    }
    else{
        console.log('wyeabao')
      return next.handle(request.clone()).pipe(
        tap(
          succ => {},
          err =>{  
              this.notificationService.error(this.errorMsg) 
          }
        )
      );
    } 
  }
}