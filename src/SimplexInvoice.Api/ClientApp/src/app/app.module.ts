import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AuthenticationInterceptor } from './shared/interceptors/authentication.interceptor';
import { AuthenticationService, ErrorService, JWTService, LoadFileService, PopupService } from './shared/services';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { HomeModule } from './modules/home';
import { IdDocumentTypesModule } from './modules/iddocumenttypes';
import { LoginComponent } from './components/login/login.component';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { NavmenuComponent, PopupComponent } from './components';
import { NgModule } from '@angular/core';
import { UsersModule } from './modules/users';
import { TaxRatesModule } from './modules/taxrates';

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
    IdDocumentTypesModule,
    TaxRatesModule
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
