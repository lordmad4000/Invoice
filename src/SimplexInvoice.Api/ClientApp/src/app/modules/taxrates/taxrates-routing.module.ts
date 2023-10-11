import { TaxRatesComponent } from './taxrates.component';
import { TaxRatesEditComponent } from './taxrates-edit';
import { TaxRatesGridComponent } from './taxrates-grid';
import { TaxRatesNewComponent } from './taxrates-new';
import { TaxRatesViewComponent } from './taxrates-view';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from 'src/app/shared/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: TaxRatesComponent,
    children: [
      { path: 'grid', component: TaxRatesGridComponent, canActivate: [authGuard] },
      { path: 'edit/:id', component: TaxRatesEditComponent, canActivate: [authGuard] },
      { path: 'new', component: TaxRatesNewComponent, canActivate: [authGuard] },
      { path: 'view/:id', component: TaxRatesViewComponent, canActivate: [authGuard] },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TaxRatesRoutingModule { }
