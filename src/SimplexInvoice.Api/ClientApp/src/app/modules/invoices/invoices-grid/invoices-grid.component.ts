import { Component, OnInit } from '@angular/core';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { InvoiceDto } from 'src/app/shared/models/invoicedto';
import { InvoicesService } from 'src/app/shared/services/invoices.service';
import { ErrorService } from 'src/app/shared';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { TableColumn } from 'src/app/shared/interfaces/tablecolumn';

@Component({
  selector: 'app-invoices-grid',
  templateUrl: './invoices-grid.component.html',
  styleUrls: ['./invoices-grid.component.css']
})

export class InvoicesGridComponent implements OnInit {

  private translate: any = (key: string) =>
    this.translateService.instant('invoices.' + key);


  headers: TableColumn[] = [
    { header: 'Id', field: 'id', visible: 'hidden', width: '0%' },
    { header: this.translate('forms.number'), field: 'number', visible: 'visible', width: '20%' },
    { header: this.translate('forms.date'), field: 'date', visible: 'visible', width: '20%' },
    { header: this.translate('forms.customerfullname'), field: 'customerFullName', visible: 'visible', width: '20%' },
    { header: this.translate('forms.total'), field: 'total', visible: 'visible', width: '20%' },
    { header: this.translate('forms.currency'), field: 'currency', visible: 'visible', width: '20%' },
  ];
  public data: any[] = [];

  constructor(
    private invoicesService: InvoicesService,
    private router: Router,
    private translateService: CustomTranslateService,
    private errorService: ErrorService,
    private snackBarService: SnackBarService) {
  }

  ngOnInit(): void {
    this.loadGridData();
  }

  loadGridData() {
    this.invoicesService.GetAll().subscribe({
      next: (res: Array<InvoiceDto>) => {
        if (res) {
          this.data = res;
        }
      },
      error: (err: HttpErrorResponse) => {
        this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
      }
    });
  }

  getRecord(event: any) {
    const row = event as InvoiceDto;
    this.router.navigate(['/invoices/view', `${row.id}`]);
  }

  addButtonClick() {
    this.router.navigate(['/invoices/new']);
  }

  backButtonClick() {
    this.router.navigate(['/home']);
  }

}
