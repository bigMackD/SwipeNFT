import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthenticationService } from '../services/authentication.service';
import { LoginRequest } from '../model/login/login.request';
import { LoginResponse } from '../model/login/login.response';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass'],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authenticationService: AuthenticationService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.constructForm();
  }

  private constructForm(): void {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  isFormValid(): boolean {
    return this.loginForm.valid;
  }

  private createRequest(): LoginRequest {
    return new LoginRequest(
      this.loginForm.get('username')!.value,
      this.loginForm.get('password')!.value
    );
  }

  private handleResponse(response: LoginResponse): void {
    localStorage.setItem('token', response.token);
    this.router.navigate(['/home']);
  }

  onSubmit() {
    const request = this.createRequest();
    this.authenticationService.login(request).subscribe((response) => {
      this.handleResponse(response);
    });
  }
}
