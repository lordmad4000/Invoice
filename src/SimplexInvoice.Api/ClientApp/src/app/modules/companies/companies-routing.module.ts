import { CompaniesComponent } from './companies.component';
import { CompaniesEditComponent } from './companies-edit';
import { CompaniesViewComponent } from './companies-view';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from 'src/app/shared/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: CompaniesComponent,
    children: [
      { path: 'edit', component: CompaniesEditComponent, canActivate: [authGuard] },
      { path: 'view', component: CompaniesViewComponent, canActivate: [authGuard] },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompaniesRoutingModule { }
