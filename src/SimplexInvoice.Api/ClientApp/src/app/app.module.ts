import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AuthenticationInterceptor } from './shared/interceptors/authentication.interceptor';
import { AuthenticationService, ErrorService, JWTService, LoadFileService, PopupService } from './shared/services';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { HomeModule } from './modules/home';
import { IdDocumentTypesModule } from './modules/iddocumenttypes';
import { LoginComponent } from './components/login/login.component';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { NavmenuComponent, PopupComponent } from './components';
import { NgModule } from '@angular/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { UsersModule } from './modules/users';
import { CustomTranslateService } from './shared/services/customtranslate.service';
import { SnackBarService } from './shared/services/snackbar.service';

@NgModule({
  declarations: [
    AppComponent,    
  ],
  imports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    BrowserModule,
    BrowserModule,
    CommonModule,
    FormsModule,
    HomeModule,
    HttpClientModule,
    IdDocumentTypesModule,
    LoginComponent,
    MatButtonModule,
    MatDialogModule,
    NavmenuComponent,
    PopupComponent,
    UsersModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [ HttpClient ]
      }
    }),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true },
    AuthenticationService,
    CustomTranslateService,
    ErrorService,
    JWTService,
    LoadFileService,
    PopupService,
    SnackBarService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
export function HttpLoaderFactory(httpClient: HttpClient) {
  return new TranslateHttpLoader(httpClient, './assets/languages/', '.json');
}
