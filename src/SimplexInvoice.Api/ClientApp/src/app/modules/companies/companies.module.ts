import { BaseGridComponent } from 'src/app/components/base-grid';
import { CommonModule } from '@angular/common';
import { CompaniesComponent } from './companies.component';
import { CompaniesEditComponent } from './companies-edit';
import { CompaniesRoutingModule } from './companies-routing.module';
import { CompaniesService } from 'src/app/shared/services/companies.service';
import { CompaniesViewComponent } from './companies-view';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatOptionModule } from '@angular/material/core';
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
    CompaniesComponent,
    CompaniesEditComponent,
    CompaniesViewComponent
  ],
  imports: [    
    BaseGridComponent,
    CommonModule,
    CompaniesRoutingModule,
    FormsModule,
    MatButtonModule,
    MatCheckboxModule,
    MatDividerModule,
    MatFormFieldModule,
    MatIconModule,
    MatListModule,
    MatInputModule,
    MatOptionModule,
    MatSelectModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule,
    MatToolbarModule,
    MatTooltipModule,
    ReactiveFormsModule,
    TranslateModule
  ],
  providers: [ 
    CompaniesService,
    CustomTranslateService
  ],
})
export class CompaniesModule { }