import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersRoutingModule } from './users-routing.module';
import { UsersComponent } from './users.component';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatToolbarModule } from '@angular/material/toolbar';
import { UsersGridComponent } from './users-grid/users-grid.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { UserService } from '../../shared/services/user.service';
import { UsersNewComponent } from './users-new';
import { UsersEditComponent } from './users-edit';
import { UsersViewComponent } from './users-view';

@NgModule({
  declarations: [
    UsersComponent,
    UsersGridComponent,
    UsersEditComponent,
    UsersNewComponent,
    UsersViewComponent
  ],
  imports: [
    CommonModule,
    UsersRoutingModule,
    MatTableModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatDividerModule,
    MatIconModule,
    MatButtonModule,
    MatTooltipModule,
    MatToolbarModule,
    MatSnackBarModule,
  ],
  providers: [ 
    UserService
  ],
})
export class UsersModule { }
