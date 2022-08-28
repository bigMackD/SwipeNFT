import {
  Observable,
  Subject,
} from 'rxjs';
import { HttpBackend, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';

import { AppSettings } from '../interfaces/app-setting.interface';

@Injectable({
  providedIn: 'root',
})
export class ConfigurationService {
  settings: AppSettings;
  private settings$ = new Subject<AppSettings>();
  private httpClient: HttpClient;
  constructor(handler: HttpBackend) {
    this.httpClient = new HttpClient(handler);
    this.load();
  }

  load(): Observable<AppSettings> {
    console.log('config loaded', this.settings);
    return this.httpClient
      .get<AppSettings>(`/assets/config/app-settings.json`)
      .pipe(
        tap((response) => {
          console.log(response)
          this.settings = response;
          this.settings$.next(response);
        })
      );
  }

  getSettings$(): Observable<AppSettings> {
    return this.settings$;
  }
}
