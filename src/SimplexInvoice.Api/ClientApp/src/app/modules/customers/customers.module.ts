import { BaseGridComponent } from 'src/app/components/base-grid';
import { CommonModule } from '@angular/common';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CustomersComponent } from './customers.component';
import { CustomersEditComponent } from './customers-edit';
import { CustomersGridComponent } from './customers-grid/customers-grid.component';
import { CustomersNewComponent } from './customers-new';
import { CustomersRoutingModule } from './customers-routing.module';
import { CustomersService } from 'src/app/shared/services/customers.service';
import { CustomersViewComponent } from './customers-view';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { NgModule } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [
    CustomersComponent,
    CustomersGridComponent,
    CustomersEditComponent,
    CustomersNewComponent,
    CustomersViewComponent
  ],
  imports: [
    CommonModule,
    CustomersRoutingModule,
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
    BaseGridComponent,
    TranslateModule
  ],
  providers: [ 
    CustomersService,
    CustomTranslateService
  ],
})
export class CustomersModule { }