import { Component, OnDestroy } from '@angular/core';
import { ErrorService } from 'src/app/shared/services';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Location } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { UserRegisterRequest } from 'src/app/shared/models/userregisterrequest';
import { UserResponse } from 'src/app/shared/models/userresponse';
import { UserService } from 'src/app/shared/services/user.service';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';

@Component({
  selector: 'app-users-new',
  templateUrl: './users-new.component.html',
  styleUrls: ['./users-new.component.css']
})

export class UsersNewComponent implements OnDestroy {

  private translate: any = (key: string) =>
    this.translateService.instant('users.' + key);

  private user: UserRegisterRequest = new UserRegisterRequest();
  formUser: FormGroup;
  formLoginError = "";
  private subscription: Subscription | undefined;

  constructor(
    private location: Location,
    private userService: UserService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    private snackBar: MatSnackBar,
    private translateService: CustomTranslateService) {

    this.formUser = this.formBuilder.group({
      email: [{ value: '', disabled: false }, Validators.required],
      password: [{ value: '', disabled: false }, Validators.required],
      confirmPassword: [{ value: '', disabled: false }, Validators.required],
      firstName: [{ value: '', disabled: false }, Validators.required],
      lastName: [{ value: '', disabled: false }, Validators.required],
    });
  }

  openSnackBar(message: string) {
    this.snackBar.open(message, '', { duration: 1 * 1000 });
  }

  checkPassword(): boolean {
    const password = this.formUser.get('password')?.value;
    const confirmPassword = this.formUser.get('confirmPassword')?.value;
    if (!password || !confirmPassword || password !== confirmPassword) {
      this.openSnackBar(this.translate('forms.password_not_match'));
      return false;
    }

    return true;
  }

  saveButtonClick() {
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
    this.location.back();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

}
