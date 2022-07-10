import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoreRoutingModule } from './core-routing.module';
import { CoreComponent } from './core.component';
import { AuthGuard } from './guards/auth.guard';
import { NavmenuComponent } from './components/navmenu/navmenu.component';
import { LoginComponent } from './components/login/login.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { AuthenticationService } from './services/authentication.service';
import { MatCardModule } from '@angular/material';

@NgModule({
  declarations: [
    CoreComponent,
    NavmenuComponent,
    LoginComponent,
    PageNotFoundComponent,
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    CommonModule,
    CoreRoutingModule,
    FormsModule,
    HttpClientModule,
    MatButtonModule,
    MatDividerModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatSidenavModule,
    MatToolbarModule,
    MatTooltipModule,
    MatCardModule,
    ReactiveFormsModule,
  ],
  exports: [
    NavmenuComponent,
    LoginComponent,
    PageNotFoundComponent,
  ],
  providers: [
    NavmenuComponent,
    AuthGuard,
    AuthenticationService
  ],
  // entryComponents: [PopupComponent],
})
export class CoreModule { }
