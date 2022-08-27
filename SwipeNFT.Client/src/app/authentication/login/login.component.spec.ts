import { AuthenticationService } from '../services/authentication.service';
import { LoginComponent } from './login.component';

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let mockAuthenticationService;


  beforeEach(async () => {
    mockAuthenticationService = jasmine.createSpyObj(['login']);

    await TestBed.configureTestingModule({
      declarations: [ LoginComponent ],
      imports: [ RouterTestingModule ],
      providers: [ 
        FormBuilder,
        { provide: AuthenticationService, useValue: mockAuthenticationService }
       ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
