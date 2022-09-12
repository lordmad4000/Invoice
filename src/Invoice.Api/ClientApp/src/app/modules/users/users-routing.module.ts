import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { UsersEditComponent } from './users-edit/users-edit.component';
import { UsersGridComponent } from './users-grid/users-grid.component';
import { UsersNewComponent } from './users-new/users-new.component';
import { UsersViewComponent } from './users-view/users-view.component';
import { UsersComponent } from './users.component';

const routes: Routes = [
  {
    path: '',
    component: UsersComponent,
    children: [
      { path: 'grid', component: UsersGridComponent, canActivate: [AuthGuard] },
      { path: 'edit/:id', component: UsersEditComponent, canActivate: [AuthGuard] },
      { path: 'new', component: UsersNewComponent, canActivate: [AuthGuard] },
      { path: 'view/:id', component: UsersViewComponent, canActivate: [AuthGuard] },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
