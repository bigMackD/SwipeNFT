import { catchError, map } from 'rxjs/operators';
import { HttpClient, HttpEvent, HttpHeaders, HttpParams, HttpResponse } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { Injectable } from "@angular/core";

import { ConfigurationService } from "./configuration.service";
import { NotificationService } from "./notification.service";

@Injectable()
export class ApiService {

    protected apiPath = '';

    constructor(
        private httpClient: HttpClient,
        private notificationService: NotificationService,
        protected configurationService: ConfigurationService
    ) {
        this.apiPath = `${this.configurationService.settings.apiUrl}`;
    }

    get<T>(url: string, params?: HttpParams): Observable<T> {
        const httpOptions: any = {
            params: params
        };

        return this.httpClient.get(this.apiPath + url, httpOptions)
            .pipe(
                catchError((httpResponse: HttpResponse<T>) => {
                    this.showErrorToast(httpResponse);
                    return this.getError(httpResponse);
                }));
    }

    postWithResponse<T>(url: string, body: any, params?: HttpParams): Observable<T> {
        const httpOptions: any = {
            headers: this.getHeaders(),
            params: params
        };
        const path = this.apiPath + url
        console.table({path, body, httpOptions })
        return this.httpClient.post<T>(this.apiPath + url, JSON.stringify(body), httpOptions)
            .pipe(
                map((response: HttpEvent<T>) => {
                    return response;
                }),
                catchError((response: HttpResponse<T>) => {
                    this.showErrorToast(response);
                    return this.getError(response);
                }));
    }

    put(url: string, body: any) {
        const httpOptions = { headers: this.getHeaders() };
         this.httpClient.put(this.apiPath + url, JSON.stringify(body), httpOptions)
            .pipe(
                map(
                    (response) => {
                    return response;
                }),
                catchError((response: HttpResponse<null>) => {
                    this.showErrorToast(response);
                    return this.getError(response);
                }));
    }

    private showErrorToast(response: HttpResponse<any>): void {
        if (response.status !== 401) {
            let message = this.getErrorMessage(response);
            if (message) {
                this.notificationService.error(message);
            }
        }
    }

    private getError(res: HttpResponse<any>): Observable<any> {
            return throwError(() => new Error(`Server error: ${res.statusText} (${res.status})`))
    }

    private getErrorMessage(response: any): string {
        const { error } = response;
        let finalMessage: string = 'Unhandled exception occurred';
        if (response) {
            if (error) {
                if(error.message){
                    finalMessage = `${error.message}`;
                }
            }
        }
        return finalMessage;
    }

    private getHeaders(): HttpHeaders {
        return new HttpHeaders({ 'Content-Type': 'application/json', 'Cache-Control': 'no-store' });
    }
}