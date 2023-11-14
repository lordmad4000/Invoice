import { Component, OnDestroy, OnInit } from '@angular/core';
import { ErrorService, TaxRatesService } from 'src/app/shared/services';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSelectChange } from '@angular/material/select';
import { ProductDto } from 'src/app/shared/models/productdto';
import { ProductRegisterRequest } from 'src/app/shared/models/productregisterrequest';
import { ProductsService } from 'src/app/shared/services/products.service';
import { Router } from '@angular/router';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';
import { TaxRateDto } from 'src/app/shared/models/taxratedto';

@Component({
  selector: 'app-products-new',
  templateUrl: './products-new.component.html',
  styleUrls: ['./products-new.component.css']
})

export class ProductsNewComponent implements OnInit, OnDestroy {

  private product: ProductRegisterRequest = new ProductRegisterRequest();
  public taxRates: TaxRateDto[] = [];
  public selectedTaxRateId: string = '';
  public formProduct: FormGroup;
  private subscription: Subscription | undefined;

  constructor(
    private productsService: ProductsService,
    private taxRateService: TaxRatesService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    private router: Router,
    private snackBarService: SnackBarService) {

    this.formProduct = this.formBuilder.group({
      name: [{ value: '', disabled: false }, Validators.required],
      value: [{ value: '', disabled: false }, Validators.required],
    });

    this.formProduct = this.formBuilder.group({
      code: [{ value: '', disabled: false }],
      name: [{ value: '', disabled: false }],
      description: [{ value: '', disabled: false }],
      packageQuantity: [{ value: 0.0, disabled: false }],
      price: [{ value: 0.0, disabled: false }],
      currency: [{ value: 'EUR', disabled: false }],
      taxRateId: [{ value: '', disabled: false }],
      taxRateName: [{ value: '', disabled: true }],
      taxRateValue: [{ value: 0, disabled: true }],
    });
  }

  ngOnInit(): void {
    this.loadTaxRates();
  }

  private loadTaxRates() {
    this.taxRateService.GetAll().subscribe({
      next: (res: TaxRateDto[]) => {
        if (res) {
          this.taxRates = res;
          this.selectedTaxRateId = res[0].id;
        }
      },
      error: (err: HttpErrorResponse) => {
        this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
      }
    })
  }

  saveButtonClick() {
    this.product.code = this.formProduct.get("code")?.value;
    this.product.name = this.formProduct.get("name")?.value;
    this.product.description = this.formProduct.get("description")?.value;
    this.product.packageQuantity = this.formProduct.get("packageQuantity")?.value;
    this.product.price = this.formProduct.get("price")?.value;
    this.product.currency = this.formProduct.get("currency")?.value;
    this.product.taxRateId = this.selectedTaxRateId;
    this.productsService.Post(this.product).subscribe({
      next: (res: ProductDto) => {
        if (res) {
          this.backButtonClick();
        }
      },
      error: (err: HttpErrorResponse) => {
        this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
      }
    });
  }

  onChangeTaxRates(event: MatSelectChange) {
    this.selectedTaxRateId = event.value;
    const taxRate = this.taxRates.find(c => c.id == this.selectedTaxRateId);
    this.formProduct.patchValue({ taxRateValue: taxRate?.value });
  }

  backButtonClick() {
    this.router.navigate(['/products/grid']);
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

}
