import { Component, OnInit } from '@angular/core';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { CustomerDto } from 'src/app/shared/models/customerdto';
import { CustomersService } from 'src/app/shared/services/customers.service';
import { ErrorService } from 'src/app/shared';
import { HttpErrorResponse } from '@angular/common/http';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { TableColumn } from 'src/app/shared/interfaces/tablecolumn';

@Component({
  selector: 'app-customers-grid',
  templateUrl: './customers-grid.component.html',
  styleUrls: ['./customers-grid.component.css']
})

export class CustomersGridComponent implements OnInit {

  private translate: any = (key: string) =>
    this.translateService.instant('customers.' + key);


  headers: TableColumn[] = [
    { header: 'Id', field: 'id', visible: 'hidden', width: '0%' },
    { header: this.translate('forms.firstname'), field: 'firstName', visible: 'visible', width: '20%' },
    { header: this.translate('forms.lastname'), field: 'lastName', visible: 'visible', width: '20%' },
    { header: this.translate('forms.iddocumentnumber'), field: 'idDocumentNumber', visible: 'visible', width: '20%' },
    { header: this.translate('forms.phone'), field: 'phone', visible: 'visible', width: '20%' },
    { header: this.translate('forms.email'), field: 'email', visible: 'visible', width: '20%' },
  ];
  public data: any[] = [];

  constructor(
    private location: Location,
    private customersService: CustomersService,
    private router: Router,
    private translateService: CustomTranslateService,
    private errorService: ErrorService,
    private snackBarService: SnackBarService) {
  }

  ngOnInit(): void {
    this.loadGridData();
  }

  loadGridData() {
    this.customersService.GetAll().subscribe({
      next: (res: Array<CustomerDto>) => {
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
    const row = event as CustomerDto;
    this.router.navigate(['/customers/view', `${row.id}`]);
  }

  addButtonClick() {
    this.router.navigate(['/customers/new']);
  }

  backButtonClick() {
    this.location.back();
  }

}
