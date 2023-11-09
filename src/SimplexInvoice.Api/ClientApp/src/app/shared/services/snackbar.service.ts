import { Injectable } from "@angular/core";
import { MatSnackBar, MatSnackBarConfig } from "@angular/material/snack-bar";

@Injectable({
    providedIn: "root"
})
export class SnackBarService {

    constructor(private snackBar: MatSnackBar) { }

    public openSnackBar(message: string = '', duration: number = 5, action: string = '', panelclass: string = ''): void {
        let config: MatSnackBarConfig = { duration: duration * 1000 };
        if (panelclass !== undefined && panelclass !== '') {
            config = { duration: duration * 1000, panelClass: [panelclass] };
        }
        this.snackBar.open(message, action, config);
    }
}