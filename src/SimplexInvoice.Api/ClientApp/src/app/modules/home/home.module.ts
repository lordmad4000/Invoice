import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home.component';
import { HomeRoutingModule } from './home-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { NgModule } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
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
    HttpClientModule,
    MatDialogModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatSnackBarModule,
    MatTableModule,
    ReactiveFormsModule,
    TranslateModule,
  ],
  exports:[
    HomeComponent,    
  ],
  providers: [
    CustomTranslateService
  ]
})
export class HomeModule { }
