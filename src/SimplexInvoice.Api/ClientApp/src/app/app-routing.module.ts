import { HomeComponent } from './modules/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { NgModule } from '@angular/core';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './shared/guards/auth.guard';
import { InvoicesNewComponent } from './modules/invoices';

const routes: Routes = [
{ path: '', redirectTo: 'login', pathMatch: 'full' },      
{ path: 'login', component: LoginComponent },
{ path: 'home', component:  HomeComponent },
{ path: 'users', loadChildren: () => import('./modules/users/users.module').then(x => x.UsersModule), canActivate: [authGuard]},
{ path: 'iddocumenttypes', loadChildren: () => import('./modules/iddocumenttypes/iddocumenttypes.module').then(x => x.IdDocumentTypesModule), canActivate: [authGuard]},
{ path: 'taxrates', loadChildren: () => import('./modules/taxrates/taxrates.module').then(x => x.TaxRatesModule), canActivate: [authGuard]},
{ path: 'companies', loadChildren: () => import('./modules/companies/companies.module').then(x => x.CompaniesModule), canActivate: [authGuard]},
{ path: 'customers', loadChildren: () => import('./modules/customers/customers.module').then(x => x.CustomersModule), canActivate: [authGuard]},
{ path: 'products', loadChildren: () => import('./modules/products/products.module').then(x => x.ProductsModule), canActivate: [authGuard]},
{ path: 'invoices', loadChildren: () => import('./modules/invoices/invoices.module').then(x => x.InvoicesModule), canActivate: [authGuard]},
{ path: 'test', component: InvoicesNewComponent },
{ path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
