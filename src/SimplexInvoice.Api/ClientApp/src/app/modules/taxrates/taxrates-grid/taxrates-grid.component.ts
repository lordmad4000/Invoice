import { Component, OnInit } from '@angular/core';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { ErrorService } from 'src/app/shared';
import { HttpErrorResponse } from '@angular/common/http';
import { TaxRateDto } from 'src/app/shared/models/taxratedto';
import { TaxRatesService } from 'src/app/shared/services/taxrates.service';
import { Router } from '@angular/router';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { TableColumn } from 'src/app/shared/interfaces/tablecolumn';

@Component({
  selector: 'app-taxrates-grid',
  templateUrl: './taxrates-grid.component.html',
  styleUrls: ['./taxrates-grid.component.css']
})

export class TaxRatesGridComponent implements OnInit {

  private translate: any = (key: string) =>
    this.translateService.instant('taxrates.' + key);


  headers: TableColumn[] = [
    { header: 'Id', field: 'id', visible: 'hidden', width: '0%' },
    { header: this.translate('forms.name'), field: 'name', visible: 'visible', width: '50%' },
    { header: this.translate('forms.value'), field: 'value', visible: 'visible', width: '50%' },
  ];
  public data: any[] = [];

  constructor(
    private taxRatesService: TaxRatesService,
    private router: Router,
    private translateService: CustomTranslateService,
    private errorService: ErrorService,
    private snackBarService: SnackBarService) {
  }

  ngOnInit(): void {
    this.loadGridData();
  }

  loadGridData() {
    this.taxRatesService.GetAll().subscribe({
      next: (res: Array<TaxRateDto>) => {
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
    const row = event as TaxRateDto;
    this.router.navigate(['/taxrates/view', `${row.id}`]);
  }

  addButtonClick() {
    this.router.navigate(['/taxrates/new']);
  }

  backButtonClick() {
    this.router.navigate(['/home']);
  }

}
