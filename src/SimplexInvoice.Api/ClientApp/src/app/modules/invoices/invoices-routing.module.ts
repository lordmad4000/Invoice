import { InvoicesComponent } from './invoices.component';
import { InvoicesGridComponent } from './invoices-grid';
import { InvoicesViewComponent } from './invoices-view';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from 'src/app/shared/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: InvoicesComponent,
    children: [
      { path: 'grid', component: InvoicesGridComponent, canActivate: [authGuard] },
      // { path: 'new', component: InvoicesNewComponent, canActivate: [authGuard] },
      { path: 'view/:id', component: InvoicesViewComponent, canActivate: [authGuard] },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InvoicesRoutingModule { }
