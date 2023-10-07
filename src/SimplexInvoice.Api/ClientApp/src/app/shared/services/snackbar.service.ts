import { Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";

@Injectable({
    providedIn: "root"
})
export class SnackBarService {

    constructor(private snackBar: MatSnackBar) { }

    public openSnackBar(message: string): void;
    public openSnackBar(message: string, duration?: number): void {
        duration = duration ?? 5;
        this.snackBar.open(message, '', { duration: duration * 1000 });
    }

}
