import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Login } from 'src/app/models/login';
import { UserLoginResponse } from 'src/app/models/userloginresponse';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  formLogin: FormGroup;
  private subscription = new Subscription();
  firstNameAutofilled: boolean = false;
  lastNameAutofilled: boolean = false;

  constructor(private formBuilder: FormBuilder,
    private httpClient: HttpClient,
    private router: Router
  ) {
    this.formLogin = formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }

  Login() {
    const usuarioLogin: Login = {
      username: this.formLogin.value.username,
      password: this.formLogin.value.password
    };

    const url = `${environment.baseHttpUrl}/User/Login`;

    this.subscription = this.httpClient
      .post<UserLoginResponse>(url, usuarioLogin, { observe: 'response' })
      .subscribe({
        next: (res) => {
          let token = res.body?.token;
          console.log('token', token);
          if (token !== undefined) {
            sessionStorage.setItem('token', token);
            this.router.navigate(['/home']);
          }
          else {
            this.router.navigate(['/pagenotfound']);
          }
        },
        error: (err) => {
          console.log('Error en el login', err);
        }
      });
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

}
