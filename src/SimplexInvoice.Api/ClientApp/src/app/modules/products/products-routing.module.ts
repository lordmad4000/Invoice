import { NgModule } from '@angular/core';
import { ProductsComponent } from './products.component';
import { ProductsEditComponent } from './products-edit';
import { ProductsGridComponent } from './products-grid';
import { ProductsNewComponent } from './products-new';
import { ProductsViewComponent } from './products-view';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from 'src/app/shared/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: ProductsComponent,
    children: [
      { path: 'grid', component: ProductsGridComponent, canActivate: [authGuard] },
      { path: 'edit/:id', component: ProductsEditComponent, canActivate: [authGuard] },
      { path: 'new', component: ProductsNewComponent, canActivate: [authGuard] },
      { path: 'view/:id', component: ProductsViewComponent, canActivate: [authGuard] },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductsRoutingModule { }
