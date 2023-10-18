import { Component, OnInit } from '@angular/core';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { ProductDto } from 'src/app/shared/models/productdto';
import { ProductsService } from 'src/app/shared/services/products.service';
import { ErrorService } from 'src/app/shared';
import { HttpErrorResponse } from '@angular/common/http';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { TableColumn } from 'src/app/shared/interfaces/tablecolumn';

@Component({
  selector: 'app-products-grid',
  templateUrl: './products-grid.component.html',
  styleUrls: ['./products-grid.component.css']
})

export class ProductsGridComponent implements OnInit {

  private translate: any = (key: string) =>
    this.translateService.instant('products.' + key);


  headers: TableColumn[] = [
    { header: 'Id', field: 'id', visible: 'hidden', width: '0%' },
    { header: this.translate('forms.code'), field: 'code', visible: 'visible', width: '20%' },
    { header: this.translate('forms.name'), field: 'name', visible: 'visible', width: '20%' },
    { header: this.translate('forms.description'), field: 'description', visible: 'visible', width: '20%' },
    { header: this.translate('forms.unitprice'), field: 'price', visible: 'visible', width: '20%' },
    { header: this.translate('forms.currency'), field: 'currency', visible: 'visible', width: '20%' },
  ];
  public data: any[] = [];

  constructor(
    private location: Location,
    private productsService: ProductsService,
    private router: Router,
    private translateService: CustomTranslateService,
    private errorService: ErrorService,
    private snackBarService: SnackBarService) {
  }

  ngOnInit(): void {
    this.loadGridData();
  }

  loadGridData() {
    this.productsService.GetAll().subscribe({
      next: (res: Array<ProductDto>) => {
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
    const row = event as ProductDto;
    this.router.navigate(['/products/view', `${row.id}`]);
  }

  addButtonClick() {
    this.router.navigate(['/products/new']);
  }

  backButtonClick() {
    this.location.back();
  }

}
