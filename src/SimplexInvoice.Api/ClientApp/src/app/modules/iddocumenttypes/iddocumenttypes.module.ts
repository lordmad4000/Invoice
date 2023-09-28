import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IdDocumentTypesComponent } from './iddocumenttypes.component';
import { IdDocumentTypesEditComponent } from './iddocumenttypes-edit';
import { IdDocumentTypesGridComponent } from './iddocumenttypes-grid/iddocumenttypes-grid.component';
import { IdDocumentTypesNewComponent } from './iddocumenttypes-new';
import { IdDocumentTypesRoutingModule } from './iddocumenttypes-routing.module';
import { IdDocumentTypesService } from 'src/app/shared/services/iddocumenttypes.service';
import { IdDocumentTypesViewComponent } from './iddocumenttypes-view';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { NgModule } from '@angular/core';
import { MatSortModule } from '@angular/material/sort';
import { BaseGridComponent } from 'src/app/components/base-grid';

@NgModule({
  declarations: [
    IdDocumentTypesComponent,
    IdDocumentTypesGridComponent,
    IdDocumentTypesEditComponent,
    IdDocumentTypesNewComponent,
    IdDocumentTypesViewComponent
  ],
  imports: [
    CommonModule,
    IdDocumentTypesRoutingModule,
    MatTableModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatDividerModule,
    MatIconModule,
    MatButtonModule,
    MatTooltipModule,
    MatToolbarModule,
    MatSnackBarModule,
    MatSortModule,
    BaseGridComponent
  ],
  providers: [ 
    IdDocumentTypesService
  ],
})
export class IdDocumentTypesModule { }