import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiService } from 'src/app/shared/services/api.service';
import { ConfigurationService } from 'src/app/shared/services/configuration.service';
import { LoginRequest } from '../model/login/login.request';
import { LoginResponse } from '../model/login/login.response';
import { RegisterRequest } from '../model/register/register.request';
import { RegisterResponse } from '../model/register/register.response';
import { UserProfileResponse } from '../model/userProfile/user-profile.response';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  protected apiUrl = '';
  protected userName: string;

  constructor(
    private apiService: ApiService,
    private configurationService: ConfigurationService
    ) { 
    this.apiUrl = `${this.configurationService.settings.apiUrl}`;
  }

  public getToken(): string {
    return localStorage.getItem('token');
  }

  public register(request:RegisterRequest):Observable<RegisterResponse>{
    return this.apiService.postWithResponse('/Users/Register', request);
  }

  public login(request:LoginRequest):Observable<LoginResponse>{
    return this.apiService.postWithResponse('/Users/Login', request);
  }

  public getUserProfile():Observable<UserProfileResponse>{
    return this.apiService.get<UserProfileResponse>('/UserProfile');
  }

  public logout():void{
    this.userName = null;
    localStorage.removeItem('token');
  }
}