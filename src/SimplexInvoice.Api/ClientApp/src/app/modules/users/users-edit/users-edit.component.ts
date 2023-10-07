import { ActivatedRoute, Params, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { ErrorService } from 'src/app/shared/services/error.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Location } from '@angular/common';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';
import { UserResponse } from 'src/app/shared/models/userresponse';
import { UserService } from 'src/app/shared/services/user.service';
import { UserUpdateRequest } from 'src/app/shared/models/userupdaterequest';

@Component({
  selector: 'app-users-edit',
  templateUrl: './users-edit.component.html',
  styleUrls: ['./users-edit.component.css']
})
export class UsersEditComponent implements OnInit, OnDestroy {

  private translate: any = (key: string) =>
    this.translateService.instant('users.' + key);

  private user: UserUpdateRequest = new UserUpdateRequest();
  public formUser: FormGroup;
  public passwordError = false;
  private subscription: Subscription | undefined;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private userService: UserService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    private router: Router,
    private snackBarService: SnackBarService,
    private translateService: CustomTranslateService) {

    this.formUser = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      email: [{ value: '', disabled: false }],
      password: [{ value: '', disabled: false }],
      confirmPassword: [{ value: '', disabled: false }],
      firstName: [{ value: '', disabled: false }],
      lastName: [{ value: '', disabled: false }],
    });
  }

  ngOnInit(): void {
    this.subscription = this.route.params.subscribe((params: Params): void => {
      const id = params['id'];
      console.log(id);
      this.getUser(id);
    });
  }

  checkPassword(): boolean {
    const password = this.formUser.get('password')?.value;
    const confirmPassword = this.formUser.get('confirmPassword')?.value;
    if (!password || !confirmPassword || password !== confirmPassword) {
      this.snackBarService.openSnackBar(this.translate('forms.password_not_match'));
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
      error: (err: HttpErrorResponse) => {
        this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
      }
    })
  }

  saveButtonClick() {
    if (this.checkPassword() === false)
      return;

    this.user.id = this.formUser.get("id")?.value;
    this.user.email = this.formUser.get("email")?.value;
    this.user.password = this.formUser.get("password")?.value;
    this.user.firstName = this.formUser.get("firstName")?.value;
    this.user.lastName = this.formUser.get("lastName")?.value;
    this.userService.Update(this.user).subscribe({
      next: (res: UserResponse) => {
        if (res) {
          this.router.navigate(['/users/view', `${res.id}`]);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
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