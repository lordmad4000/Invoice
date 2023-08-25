import { Location } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserRegisterRequest } from 'src/app/shared/models/userregisterrequest';
import { UserResponse } from 'src/app/shared/models/userresponse';
import { ErrorService } from 'src/app/shared/services';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-users-new',
  templateUrl: './users-new.component.html',
  styleUrls: ['./users-new.component.css']
})

export class UsersNewComponent implements OnDestroy {

  private user: UserRegisterRequest = new UserRegisterRequest();
  formUser: FormGroup;
  formLoginError = "";
  private subscription: Subscription | undefined;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private userService: UserService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    private router: Router,
    private snackBar: MatSnackBar) {

    this.formUser = formBuilder.group({
      email: [{ value: '', disabled: false }, Validators.required ],
      password: [{ value: '', disabled: false }, Validators.required],
      confirmPassword: [{ value: '', disabled: false }, Validators.required],
      firstName: [{ value: '', disabled: false }, Validators.required],
      lastName: [{ value: '', disabled: false }, Validators.required],
    });
  }

  openSnackBar(message: string) {
    this.snackBar.open(message, '', { duration: 1 * 1000 });
  }

  checkPassword() : boolean{
    const password = this.formUser.get('password')?.value;
    const confirmPassword = this.formUser.get('confirmPassword')?.value;
    if (!password || !confirmPassword || password !== confirmPassword) {
      this.openSnackBar('Password not match');
      return false;
    }

    return true;
  }

  private getUser(id: string) {
    this.userService.Get(id).subscribe({
      next: (res: UserResponse) => {
        if (res) {
          this.user = res;
          this.formUser.patchValue(res);
        }
      },
      error: (err : HttpErrorResponse) => {
        console.log('Error al recuperar el usuario', err);
      }
    })
  }


  saveButtonClick() {
    console.log("Save button.");
    if (this.checkPassword() === false)
      return;

    this.user.password = this.formUser.get("password")?.value;
    this.user.firstName = this.formUser.get("firstName")?.value;
    this.user.lastName = this.formUser.get("lastName")?.value;
    this.user.email = this.formUser.get("email")?.value;

    this.userService.Post(this.user).subscribe({
      next: (res: UserResponse) => {
        if (res) {
          this.location.back();
        }
      },
      error: (err: HttpErrorResponse) => {
        const errors = this.errorService.GetErrorsFromHttp(err);
        if (errors.length > 0) {
          errors.forEach(clientError => {
            console.log(clientError);
            this.openSnackBar(clientError);
          });
        }
      }
    });

  }

  backButtonClick() {
    console.log("Back button.");
    this.location.back();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }

  }

}
