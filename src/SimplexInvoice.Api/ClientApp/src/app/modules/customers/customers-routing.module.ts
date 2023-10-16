import { CustomersComponent } from './customers.component';
import { CustomersEditComponent } from './customers-edit';
import { CustomersGridComponent } from './customers-grid';
import { CustomersNewComponent } from './customers-new';
import { CustomersViewComponent } from './customers-view';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from 'src/app/shared/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: CustomersComponent,
    children: [
      { path: 'grid', component: CustomersGridComponent, canActivate: [authGuard] },
      { path: 'edit/:id', component: CustomersEditComponent, canActivate: [authGuard] },
      { path: 'new', component: CustomersNewComponent, canActivate: [authGuard] },
      { path: 'view/:id', component: CustomersViewComponent, canActivate: [authGuard] },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomersRoutingModule { }
