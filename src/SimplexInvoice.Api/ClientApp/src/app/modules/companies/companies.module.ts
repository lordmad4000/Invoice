import { BaseGridComponent } from 'src/app/components/base-grid';
import { CommonModule } from '@angular/common';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CompaniesComponent } from './companies.component';
import { CompaniesEditComponent } from './companies-edit';
import { CompaniesRoutingModule } from './companies-routing.module';
import { CompaniesService } from 'src/app/shared/services/companies.service';
import { CompaniesViewComponent } from './companies-view';
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
    CompaniesComponent,
    CompaniesEditComponent,
    CompaniesViewComponent
  ],
  imports: [
    CommonModule,
    CompaniesRoutingModule,
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
    CompaniesService,
    CustomTranslateService
  ],
})
export class CompaniesModule { }