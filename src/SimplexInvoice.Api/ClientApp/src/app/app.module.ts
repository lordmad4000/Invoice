import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { NavmenuComponent, PopupComponent } from './components';
import { MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { LoginComponent } from './components/login/login.component';
import { UsersModule } from './modules/users';
import { AuthenticationInterceptor } from './shared/interceptors/authentication.interceptor';
import { HomeModule } from './modules/home';
import { AuthenticationService, ErrorService, JWTService, LoadFileService, PopupService } from './shared/services';

@NgModule({
  declarations: [
    AppComponent,    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    BrowserModule,
    MatDialogModule,
    CommonModule,
    MatButtonModule,
    UsersModule,
    HomeModule,
    NavmenuComponent,
    PopupComponent,
    LoginComponent,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true },
    PopupService,
    JWTService,
    AuthenticationService,
    LoadFileService,
    ErrorService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
