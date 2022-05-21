import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageNotFoundComponent } from 'src/app/core/components/page-not-found/page-not-found.component';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { UsersEditComponent } from './users-edit/users-edit.component';
import { UsersNewComponent } from './users-new/users-new.component';
import { UsersViewComponent } from './users-view/users-view.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'users/edit', component: UsersEditComponent , canActivate: [AuthGuard] },
  { path: 'users/new', component: UsersNewComponent , canActivate: [AuthGuard] },
  { path: 'users/view', component: UsersViewComponent , canActivate: [AuthGuard] },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
