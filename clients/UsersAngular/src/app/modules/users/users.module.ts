import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsersRoutingModule } from './users-routing.module';
import { UsersViewComponent } from './users-view/users-view.component';
import { UsersEditComponent } from './users-edit/users-edit.component';
import { UsersNewComponent } from './users-new/users-new.component';
import { UsersComponent } from './users.component';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule } from '@angular/forms';
import { Userservice } from 'src/app/shared/services/userservice';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [
    UsersViewComponent,
    UsersEditComponent,
    UsersNewComponent,
    UsersComponent
  ],
  imports: [
    CommonModule,
    UsersRoutingModule,
    MatTableModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatSelectModule,
    FormsModule,
    SharedModule,
  ]
})
export class UsersModule { }
