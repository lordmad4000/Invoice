import { IdDocumentTypesComponent } from './iddocumenttypes.component';
import { IdDocumentTypesEditComponent } from './iddocumenttypes-edit';
import { IdDocumentTypesGridComponent } from './iddocumenttypes-grid';
import { IdDocumentTypesNewComponent } from './iddocumenttypes-new';
import { IdDocumentTypesViewComponent } from './iddocumenttypes-view';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from 'src/app/shared/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: IdDocumentTypesComponent,
    children: [
      { path: 'grid', component: IdDocumentTypesGridComponent, canActivate: [authGuard] },
      { path: 'edit/:id', component: IdDocumentTypesEditComponent, canActivate: [authGuard] },
      { path: 'new', component: IdDocumentTypesNewComponent, canActivate: [authGuard] },
      { path: 'view/:id', component: IdDocumentTypesViewComponent, canActivate: [authGuard] },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class IdDocumentTypesRoutingModule { }
