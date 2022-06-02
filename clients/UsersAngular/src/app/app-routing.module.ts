import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './core/components/login/login.component';
import { PageNotFoundComponent } from './core/components/page-not-found/page-not-found.component';
import { AuthGuard } from './core/guards/auth.guard';
import { HomeComponent } from './modules/home/home.component';
import { UsersEditComponent } from './modules/users/users-edit/users-edit.component';
import { UsersGridComponent } from './modules/users/users-grid/users-grid.component';
import { UsersNewComponent } from './modules/users/users-new/users-new.component';
import { UsersViewComponent } from './modules/users/users-view/users-view.component';
import { UsersComponent } from './modules/users/users.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent , canActivate: [AuthGuard] },
  { path: 'users', component: UsersComponent, canActivate: [AuthGuard]},
  { path: 'users/grid', component: UsersGridComponent , canActivate: [AuthGuard] },
  { path: 'users/edit/:id', component: UsersEditComponent , canActivate: [AuthGuard] },
  { path: 'users/new', component: UsersNewComponent , canActivate: [AuthGuard] },
  { path: 'users/view/:id', component: UsersViewComponent , canActivate: [AuthGuard] },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
