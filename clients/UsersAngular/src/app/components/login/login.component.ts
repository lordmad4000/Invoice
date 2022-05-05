import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserLoginResponse } from 'src/app/models/userloginresponse';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  formLogin: FormGroup;
  private subscription = new Subscription();

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
    const usuarioLogin: ILogin = {
      Username: this.formLogin.value.username,
      Password: this.formLogin.value.password
    };

    this.subscription = this.httpClient.post<UserLoginResponse>('https://localhost:5001/api/User/Login', usuarioLogin, { observe: 'response' })    
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
        error: (err) =>{
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
