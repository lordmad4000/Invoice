import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserService } from 'src/app/shared/services/user.service';
import { UserLoginResponse } from '../../models/userloginresponse';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit, OnDestroy {
  formLogin: FormGroup;
  formLoginError: string = '';
  private subscription: Subscription | undefined;

  constructor(
    private formBuilder: FormBuilder,
    private userService : UserService,
    private router: Router
  ) {
    this.formLogin = formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  ngOnInit(): void {}

  Login() {

    this.userService
      .Login(this.formLogin.value.username, this.formLogin.value.password)
      .subscribe({
        next: (res: UserLoginResponse) => {
          let token = res.token;
          console.log('token', token);
          if (token !== undefined) {
            sessionStorage.setItem('token', token);
            this.router.navigate(['/home']);
          } else {
            this.router.navigate(['/pagenotfound']);
          }
        },
        error: (err: HttpErrorResponse) => {
          console.log('Error en el login', err);
          this.formLoginError = 'Incorrect username or password.';
        },
      });
  }

  newAccountButtonClick(event: any) {
    console.log('New account button.');
    this.router.navigate(['/users/new']);
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
