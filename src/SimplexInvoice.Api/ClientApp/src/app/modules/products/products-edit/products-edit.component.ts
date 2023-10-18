import { ActivatedRoute, Params, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ErrorService } from 'src/app/shared/services/error.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSelectChange } from '@angular/material/select';
import { ProductDto } from 'src/app/shared/models/productdto';
import { ProductsService } from 'src/app/shared/services/products.service';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';
import { TaxRateDto } from 'src/app/shared/models/taxratedto';
import { TaxRatesService } from 'src/app/shared';

@Component({
  selector: 'app-products-edit',
  templateUrl: './products-edit.component.html',
  styleUrls: ['./products-edit.component.css']
})
export class ProductsEditComponent implements OnInit, OnDestroy {

  private product: ProductDto = new ProductDto();
  public taxRates: TaxRateDto[] = [];
  public selectedTaxRateId: string = '';  
  public formProduct: FormGroup;
  private subscription: Subscription | undefined;

  constructor(
    private route: ActivatedRoute,
    private productsService: ProductsService,
    private taxRateService: TaxRatesService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    private router: Router,
    private snackBarService: SnackBarService) {

    this.formProduct = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      code: [{ value: '', disabled: false }],
      name: [{ value: '', disabled: false }],
      description: [{ value: '', disabled: false }],
      packageQuantity: [{ value: 0.0, disabled: false }],
      price: [{ value: 0.0, disabled: false }],
      currency: [{ value: '', disabled: false }],
      taxRateId: [{ value: '', disabled: false }],
      taxRateName: [{ value: '', disabled: true }],
      taxRateValue: [{ value: 0, disabled: true }],
    });
  }

  ngOnInit(): void {
    this.loadTaxRates();
    this.subscription = this.route.params.subscribe((params: Params): void => {
      const id = params['id'];
      this.getProduct(id);
    });
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

  private getProduct(id: string) {
    this.productsService.Get(id).subscribe({
      next: (res: ProductDto) => {
        if (res) {
          this.product = res;
          this.formProduct.patchValue(res);
          this.formProduct.patchValue({ taxRateName: res.taxRate.name });
          this.formProduct.patchValue({ taxRateValue: res.taxRate.value });
          const taxRate = this.taxRates.find(c => c.id == res.taxRateId);
          this.selectedTaxRateId = taxRate?.id ?? "";
          console.log("");
        }
      },
      error: (err: HttpErrorResponse) => {
        this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
      }
    })
  }

  saveButtonClick() {
    this.product.id = this.formProduct.get("id")?.value;
    this.product.code = this.formProduct.get("code")?.value;
    this.product.name = this.formProduct.get("name")?.value;
    this.product.description = this.formProduct.get("description")?.value;
    this.product.packageQuantity = this.formProduct.get("packageQuantity")?.value;
    this.product.price = this.formProduct.get("price")?.value;
    this.product.currency = this.formProduct.get("currency")?.value;
    this.product.taxRateId = this.selectedTaxRateId;
    this.productsService.Update(this.product).subscribe({
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
    this.router.navigate(['/products/view', `${this.product.id}`]);
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }

  }

}