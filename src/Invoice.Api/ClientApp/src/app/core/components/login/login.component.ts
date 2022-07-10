import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { LoadFileService } from 'src/app/shared/services/loadfile.service';
import { UserLoginResponse } from '../../models/userloginresponse';
import { AuthenticationService } from '../../services/authentication.service';

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
    private authenticationService : AuthenticationService,
    private router: Router,
    private loadFileService: LoadFileService,
  ) {

    this.formLogin = new FormGroup({
      version: new FormControl( { value: '1.0.0', disabled: true }, Validators.required),
      username: new FormControl( '', Validators.required),
      password: new FormControl( '', Validators.required),
    })

  }

  ngOnInit(): void {    
  }

  Login() {    
    this.authenticationService
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
          const error = new String(err.error).valueOf();
          if (error.includes('html') || !error){
            this.formLoginError = "Unexpected error";
          }
          else {
            this.formLoginError = err.error;
          }

        },
      });
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
