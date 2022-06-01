import { Location } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserDto } from 'src/app/shared/models/userdto';
import { ErrorService } from 'src/app/shared/services/error.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-users-new',
  templateUrl: './users-new.component.html',
  styleUrls: ['./users-new.component.css']
})

export class UsersNewComponent implements OnInit, OnDestroy {

  private user: UserDto = new UserDto();
  formUser: FormGroup;
  formLoginError: string = "";
  private subscription: Subscription | undefined;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private userService: UserService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    private router: Router) {

    this.formUser = formBuilder.group({
      userName: [{ value: '', disabled: false }],
      password: [{ value: '', disabled: false }],
      firstName: [{ value: '', disabled: false }],
      lastName: [{ value: '', disabled: false }],
      email: [{ value: '', disabled: false }],
    });
  }

  ngOnInit(): void {
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
    console.log("Save button.");
    this.user.userName = this.formUser.get("userName")?.value;
    this.user.password = this.formUser.get("password")?.value;
    this.user.firstName = this.formUser.get("firstName")?.value;
    this.user.lastName = this.formUser.get("lastName")?.value;
    this.user.email = this.formUser.get("email")?.value;

    this.userService.Post(this.user).subscribe({
      next: (res: any) => {
        const data = res;
      },
      error: (err: HttpErrorResponse) => {
        var errors = this.errorService.GetErrorsFromHttp(err);
        errors.forEach(clientError => {
          console.log(clientError);
        });
      }
    });

  }

  backButtonClick(event: any) {
    console.log("Back button.");
    this.location.back();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }

  }

}
