import { BaseGridComponent } from 'src/app/components/base-grid';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule, DatePipe } from '@angular/common';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InvoicesComponent } from './invoices.component';
import { InvoicesGridComponent } from './invoices-grid/invoices-grid.component';
import { InvoicesNewComponent } from './invoices-new';
import { InvoicesRoutingModule } from './invoices-routing.module';
import { InvoicesService } from 'src/app/shared/services/invoices.service';
import { InvoicesViewComponent } from './invoices-view';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MoneyService } from 'src/app/shared/services/money.service';
import { NgModule } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { SearchComponent } from 'src/app/components/search/search.component';

@NgModule({
  declarations: [
    InvoicesComponent,
    InvoicesGridComponent,
    InvoicesNewComponent,
    InvoicesViewComponent
  ],
  imports: [
    BaseGridComponent,
    CommonModule,
    DragDropModule,
    FormsModule,
    InvoicesRoutingModule,
    MatButtonModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatDividerModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatSelectModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule,
    MatToolbarModule,
    MatTooltipModule,
    ReactiveFormsModule,
    SearchComponent,
    TranslateModule,
  ],
  providers: [
    InvoicesService,
    CustomTranslateService,
    MoneyService,
    DatePipe
  ],
})
export class InvoicesModule { }