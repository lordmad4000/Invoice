import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from './core/core.module';
import { HomeModule } from './modules/home/home.module';
import { UsersModule } from './modules/users/users.module';
import { PopupComponent } from './core/components/popup/popup.component';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    CoreModule,
    HomeModule,
    UsersModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents:[ PopupComponent ]
})
export class AppModule { }
