import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './core/components/login/login.component';
import { PageNotFoundComponent } from './core/components/page-not-found/page-not-found.component';
import { AuthGuard } from './core/guards/auth.guard';
import { HomeComponent } from './modules/home/home.component';
import { UsersComponent } from './modules/users/users.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent , canActivate: [AuthGuard] },
  { path: 'users', component: UsersComponent, canActivate: [AuthGuard]},
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
