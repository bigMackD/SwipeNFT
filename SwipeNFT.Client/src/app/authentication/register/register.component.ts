import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AuthenticationService } from '../services/authentication.service';
import { CustomValidators } from '../validators/custom-validators';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { RegisterRequest } from '../model/register/register.request';
import { RegisterResponse } from '../model/register/register.response';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.sass']
})
export class RegisterComponent implements OnInit {

  registerForm:FormGroup;

  constructor(private fb: FormBuilder,
    private authenticationService:AuthenticationService,
    private notificationService:NotificationService,
    private router: Router
   ) { }

  ngOnInit(): void {
    this.constructForm();
  }

  
  private constructForm(): void{
    this.registerForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")]],
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.pattern('^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$')]],
      confirmPassword: ['', [Validators.required]]
    },
    {
      validators: [CustomValidators.passwordMatchValidator]
    });
  }

  isFormValid():boolean{
    return this.registerForm.valid;
  }

  private createRequest():RegisterRequest{
    return new RegisterRequest(
      this.registerForm.get('username').value,
      this.registerForm.get('email').value,
      this.registerForm.get('fullName').value,
      this.registerForm.get('password').value,
    );
  }

  private handleResponse(response:RegisterResponse): void{
      this.notificationService.success("Successfully registered!");
      this.router.navigate(['/login']);
  }

  onSubmit(): void{
    const request = this.createRequest();
    this.authenticationService.register(request).subscribe(response =>
      this.handleResponse(response)
      );
  }

}
