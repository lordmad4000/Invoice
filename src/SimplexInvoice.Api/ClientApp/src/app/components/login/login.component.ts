import { AuthenticationService } from '../../shared/services/authentication.service';
import { CommonModule } from '@angular/common';
import { Component, OnDestroy } from '@angular/core';
import { ErrorService } from 'src/app/shared';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';
import { TranslateModule } from '@ngx-translate/core';
import { UserLoginResponse } from '../../shared/models/userloginresponse';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSnackBarModule,
    ReactiveFormsModule,
    TranslateModule
  ],
})
export class LoginComponent implements OnDestroy {
  formLogin: FormGroup;
  formLoginError = '';
  private subscription: Subscription | undefined;

  constructor(
    private authenticationService: AuthenticationService,
    private router: Router,
    private snackBarService: SnackBarService,
    private errorService: ErrorService
  ) {
    this.formLogin = new FormGroup({
      version: new FormControl({ value: '1.0.0', disabled: true }, Validators.required),
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    })

  }

  Login() {
    this.authenticationService
      .Login(this.formLogin.value.email, this.formLogin.value.password)
      .subscribe({
        next: (res: UserLoginResponse) => {
          const token = res.token;
          console.log('token', token);
          if (token !== undefined) {
            sessionStorage.setItem('token', token);
            this.router.navigate(['/home']);
          } else {
            this.router.navigate(['/pagenotfound']);
          }
        },
        error: (err: HttpErrorResponse) => {
          this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
        },
      });
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
