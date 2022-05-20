import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsersRoutingModule } from './users-routing.module';
import { UsersViewComponent } from './users-view/users-view.component';
import { UsersEditComponent } from './users-edit/users-edit.component';
import { UsersNewComponent } from './users-new/users-new.component';
import { UsersComponent } from './users.component';


@NgModule({
  declarations: [
    UsersViewComponent,
    UsersEditComponent,
    UsersNewComponent,
    UsersComponent
  ],
  imports: [
    CommonModule,
    UsersRoutingModule
  ]
})
export class UsersModule { }
