import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersGridComponent } from './users-grid/users-grid.component';
import { UsersComponent } from './users.component';
import { authGuard } from 'src/app/shared/guards/auth.guard';
import { UsersNewComponent } from './users-new';
import { UsersEditComponent } from './users-edit';
import { UsersViewComponent } from './users-view';

const routes: Routes = [
  {
    path: '',
    component: UsersComponent,
    children: [
      { path: 'grid', component: UsersGridComponent, canActivate: [authGuard] },
      { path: 'edit/:id', component: UsersEditComponent, canActivate: [authGuard] },
      { path: 'new', component: UsersNewComponent, canActivate: [authGuard] },
      { path: 'view/:id', component: UsersViewComponent, canActivate: [authGuard] },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
