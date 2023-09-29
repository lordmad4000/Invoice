import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TaxRatesComponent } from './taxrates.component';
import { TaxRatesGridComponent } from './taxrates-grid';
import { TaxRatesViewComponent } from './taxrates-view';
import { authGuard } from 'src/app/shared/guards/auth.guard';
import { TaxRatesNewComponent } from './taxrates-new';

const routes: Routes = [
  {
    path: '',
    component: TaxRatesComponent,
    children: [
      { path: 'grid', component: TaxRatesGridComponent, canActivate: [authGuard] },
      { path: 'view/:id', component: TaxRatesViewComponent, canActivate: [authGuard] },
      { path: 'new', component: TaxRatesNewComponent, canActivate: [authGuard] },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TaxRatesRoutingModule { }
