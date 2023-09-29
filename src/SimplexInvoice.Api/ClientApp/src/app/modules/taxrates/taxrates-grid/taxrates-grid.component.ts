import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { TableColumn } from 'src/app/shared/interfaces/tablecolumn';
import { TaxRateDto } from 'src/app/shared/models/taxratedto';
import { TaxRatesService } from 'src/app/shared/services/taxrates.service';

@Component({
  selector: 'app-taxrates-grid',
  templateUrl: './taxrates-grid.component.html',
  styleUrls: ['./taxrates-grid.component.css']
})

export class TaxRatesGridComponent implements OnInit {

  headers : TableColumn[] = [
    { header : 'Id', field : 'id', visible: 'hidden', width: '0%' },
    { header : 'Name', field : 'name', visible: 'visible', width: '50%' },
    { header : 'Value', field : 'value', visible: 'visible', width: '50%' },
  ];
  data: any[] = [];

  constructor(
    private taxratesservice: TaxRatesService,
    private router: Router) {
  }

  ngOnInit(): void {
    this.loadGridData();
  }

  loadGridData() {
    this.taxratesservice.GetAll().subscribe({
      next: (res: Array<TaxRateDto>) => {
        if (res) {
          this.data = res;
        }
      },
      error: (err: HttpErrorResponse) => {
        console.log('Error al recuperar los Tax Rates', err);
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
  
}
