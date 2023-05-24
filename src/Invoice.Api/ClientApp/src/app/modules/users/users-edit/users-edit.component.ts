import { Location } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserUpdateRequest } from 'src/app/shared/models/userupdaterequest';
import { ErrorService } from 'src/app/shared/services/error.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-users-edit',
  templateUrl: './users-edit.component.html',
  styleUrls: ['./users-edit.component.css']
})
export class UsersEditComponent implements OnInit, OnDestroy {

  private user: UserUpdateRequest = new UserUpdateRequest();
  public formUser: FormGroup;
  private formLoginError = '';
  public passwordError = false;
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

  openSnackBar(message: string) {
      this.snackBar.open(message,'', { duration: 1 * 1000 });
  }

  checkPassword() : boolean {
    const password = this.formUser.get('password')!.value;
    const confirmPassword = this.formUser.get('confirmPassword')!.value;
    if (!password || !confirmPassword || password !== confirmPassword) {
      this.openSnackBar('Password not match');
      return false;
    }

    return true;
  }

  private getUser(id: string) {
    this.userService.Get(id).subscribe({
      next: (res: any) => {
        const data = res;
        if (data) {
          this.user = data;
          this.formUser.patchValue(data);
        }
      },
      error: (err : HttpErrorResponse) => {
        console.log('Error al recuperar el usuario', err);
      }
    })
  }

  saveButtonClick(event: any) {
    console.log('Save button.');
    if (this.checkPassword() === false)
      return;

    this.user.id = this.formUser.get("id")!.value;
    this.user.email = this.formUser.get("email")!.value;
    this.user.password = this.formUser.get("password")!.value;
    this.user.firstName = this.formUser.get("firstName")!.value;
    this.user.lastName = this.formUser.get("lastName")!.value;
    this.userService.Update(this.user).subscribe({
      next: (res: any) => {
        const data = res;
        this.router.navigate(['/users/view', `${res.id}`]);
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

  backButtonClick(event: any) {
    console.log('Back button.');
    this.location.back();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }

  }

}