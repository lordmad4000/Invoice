import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedRoutingModule } from './shared-routing.module';
import { UserService } from './services/user.service';
import { JWTService } from './services/jwt.service';
import { ErrorService } from './services/error.service';
import { MatDialogClose, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatToolbarModule } from '@angular/material/toolbar';
import { PopupComponent } from './components/popup/popup.component';

@NgModule({
  declarations: [  
    PopupComponent
  ],
  imports: [
    CommonModule,
    SharedRoutingModule
  ],
  exports: [
    PopupComponent,
  ],
  providers: [
    UserService,
    JWTService,
    ErrorService,
    MatDialogModule,
    MatButtonModule,    
    MatTableModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatSelectModule,
    FormsModule,
    SharedModule,
    ReactiveFormsModule,
    MatInputModule,
    MatDividerModule,
    MatIconModule,
    MatTooltipModule,
    MatToolbarModule,
    MatDialogClose,
    MatDialogModule,
  ]
})
export class SharedModule { }
