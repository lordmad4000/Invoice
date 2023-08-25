import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { authGuard } from './shared/guards/auth.guard';
import { HomeComponent } from './modules/home/home.component';

const routes: Routes = [
{ path: '', redirectTo: 'login', pathMatch: 'full' },      
{ path: 'login', component: LoginComponent },
{ path: 'home', component:  HomeComponent },
{ path: 'users', loadChildren: () => import('./modules/users/users.module').then(x => x.UsersModule), canActivate: [authGuard]},
{ path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
