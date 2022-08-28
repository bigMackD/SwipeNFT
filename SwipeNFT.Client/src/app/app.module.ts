import {
  APP_INITIALIZER,
  CUSTOM_ELEMENTS_SCHEMA,
  NgModule,
} from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ApiService } from './shared/services/api.service';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { ConfigurationService } from './shared/services/configuration.service';
import { ENVIRONMENT } from './shared/services/enviroment.service';
import { environment } from 'src/environments/environment';

import { HomeComponent } from './home/home.component';
import { LoginComponent } from './authentication/login/login.component';
import { MaterialModule } from './material/material.module';
import { RegisterComponent } from './authentication/register/register.component';
import { TokenInterceptor } from './shared/interceptors/token.interceptor';

export function initApp(configurationService: ConfigurationService) {
  return () => configurationService.load();
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MaterialModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    {
      provide: ENVIRONMENT,
      useValue: environment,
    },
    {
      provide: APP_INITIALIZER,
      useFactory: initApp,
      multi: true,
      deps: [ConfigurationService],
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
      deps: [APP_INITIALIZER],
    },
    ApiService,
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule {}
