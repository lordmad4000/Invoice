import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserLoginResponse } from '../../shared/models/userloginresponse';
import { AuthenticationService } from '../../shared/services/authentication.service';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

@Component({
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    CommonModule
  ], 

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
    private router: Router
  ) {

    this.formLogin = new FormGroup({
      version: new FormControl( { value: '1.0.0', disabled: true }, Validators.required),
      email: new FormControl( '', Validators.required),
      password: new FormControl( '', Validators.required),
    })

  }

  ngOnInit(): void {    
  }

  Login() {    
    this.authenticationService
      .Login(this.formLogin.value.email, this.formLogin.value.password)
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
