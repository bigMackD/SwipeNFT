import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';


@Injectable({
    providedIn: 'root'
})
export class NotificationService{

    constructor(private snackBar: MatSnackBar) {}

    public error(message:string):void{
        this.snackBar.open(message,'', {
            duration: 5000,
            verticalPosition: 'bottom',
            horizontalPosition: 'left',
            panelClass: ['mat-toolbar', 'mat-warn']
          });
    }

    public success(message:string):void{
        this.snackBar.open(message,'', {
            duration: 5000,
            verticalPosition: 'bottom',
            horizontalPosition: 'left',
            panelClass: ['mat-toolbar', 'mat-success']
          });
    }
}