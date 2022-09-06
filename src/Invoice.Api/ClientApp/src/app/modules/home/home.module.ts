import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { MatDialogModule } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatNativeDateModule, MatTableModule } from '@angular/material';
import {DragDropModule} from '@angular/cdk/drag-drop';

@NgModule({
  declarations: [
    HomeComponent,
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    CommonModule,
    DragDropModule,    
    FormsModule,
    HomeRoutingModule,
    MatTableModule,
    HttpClientModule,
    MatDialogModule,
    MatNativeDateModule,
    ReactiveFormsModule,    
  ],
  exports:[
    HomeComponent,
  ]
})
export class HomeModule { }
