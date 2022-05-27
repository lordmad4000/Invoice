import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { JsonDocument } from 'src/app/shared/models/jsondocument';
import { UserDto } from 'src/app/shared/models/userdto';
import { ErrorService } from 'src/app/shared/services/error.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-users-edit',
  templateUrl: './users-edit.component.html',
  styleUrls: ['./users-edit.component.css']
})
export class UsersEditComponent implements OnInit, OnDestroy {

  private user: UserDto = new UserDto();
  formUser: FormGroup;
  formLoginError: string = "";
  private subscription: Subscription | undefined;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    private router: Router) {

    this.formUser = formBuilder.group({
      id: [{ value: '', disabled: true }],
      userName: [{ value: '', disabled: true }],
      password: [{ value: '', disabled: true }],
      firstName: [{ value: '', disabled: false }],
      lastName: [{ value: '', disabled: false }],
      email: [{ value: '', disabled: false }],
    });
  }

  ngOnInit(): void {
    this.subscription = this.route.params.subscribe((params: Params): void => {
      const id = params['id'];
      console.log(id);
      this.getUser(id);
    });
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

    const jsonDocument: JsonDocument[] = [];

    if (this.user.firstName != this.formUser.get("firstName")?.value) {
      jsonDocument.push({ value: this.formUser.get("firstName")?.value, path: "/firstname/", op: "replace", from: "" });
    }

    if (this.user.lastName != this.formUser.get("lastName")?.value) {
      jsonDocument.push({ value: this.formUser.get("lastName")?.value, path: "/lastname/", op: "replace", from: "" });
    }

    this.userService.Patch(jsonDocument, this.formUser.get("id")?.value).subscribe({
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

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }

  }

}