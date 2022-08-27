import { ApiService } from 'src/app/shared/services/api.service';
import { AppSettings } from 'src/app/shared/interfaces/app-setting.interface';
import { AuthenticationService } from './authentication.service';
import { ConfigurationService } from 'src/app/shared/services/configuration.service';

import { TestBed } from '@angular/core/testing';


describe('AuthenticationService', () => {
  let service: AuthenticationService;
  let mockApiService;
  let mockConfigurationService;

  beforeEach(() => {

    mockApiService = jasmine.createSpyObj<ApiService>(['get', 'postWithResponse']);
    mockConfigurationService = jasmine.createSpyObj<ConfigurationService>(['load']);

    let sett:AppSettings =  {
        apiUrl: 'test'
    };

    mockConfigurationService.settings = sett;

    TestBed.configureTestingModule({
      providers: [
        { provide: ApiService, useValue: mockApiService },
        { provide: ConfigurationService, useValue: mockConfigurationService }
      ],
    })
    service = TestBed.inject(AuthenticationService);
  });

  
  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

