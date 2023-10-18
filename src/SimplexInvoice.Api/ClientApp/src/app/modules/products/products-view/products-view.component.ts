import { ActivatedRoute, Params, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { ProductDto } from 'src/app/shared/models/productdto';
import { ProductsService } from 'src/app/shared/services/products.service';
import { ErrorService, PopupService } from 'src/app/shared';
import { FormBuilder, FormGroup } from '@angular/forms';
import { GlobalConstants } from 'src/app/shared/const/global-constants';
import { HttpErrorResponse } from '@angular/common/http';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-products-view',
  templateUrl: './products-view.component.html',
  styleUrls: ['./products-view.component.css']
})
export class ProductsViewComponent implements OnInit, OnDestroy {

  public formProduct: FormGroup;
  private subscription: Subscription | undefined;
  private id = "";

  constructor(
    private route: ActivatedRoute,
    private productsService: ProductsService,
    private formBuilder: FormBuilder,
    private popupService: PopupService,
    private router: Router,
    private translateService: CustomTranslateService,
    private errorService: ErrorService,
    private snackBarService: SnackBarService) {

    this.formProduct = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      code: [{ value: '', disabled: true }],
      name: [{ value: '', disabled: true }],
      description: [{ value: '', disabled: true }],
      packageQuantity: [{ value: 0.0, disabled: true }],
      price: [{ value: 0.0, disabled: true }],
      currency: [{ value: '', disabled: true }],
      taxRateName: [{ value: '', disabled: true }],
      taxRateValue: [{ value: 0, disabled: true }],
    });

  }

  ngOnInit(): void {
    this.subscription = this.route.params.subscribe((params: Params): void => {
      this.id = params['id'];
      this.getProduct(this.id);
    });
  }

  private getProduct(id: string) {
    this.productsService.Get(id)
      .subscribe({
        next: (res: ProductDto) => {
          if (res) {
            this.formProduct.patchValue(res);
            this.formProduct.patchValue({ taxRateName: res.taxRate.name });
            this.formProduct.patchValue({ taxRateValue: res.taxRate.value });
          }
        },
        error: (err: HttpErrorResponse) => {
          this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
        }
      })
  }

  backButtonClick() {
    this.router.navigate(['/products/grid']);
  }

  editButtonClick() {
    this.router.navigate(['/products/edit', `${this.id} `]);
  }

  deleteButtonClick() {
    this.popupService
      .createConfirmPopup(this.translateService.instant('shared.popup.delete_title'), this.translateService.instant('shared.popup.delete_message'))
      .afterClosed()
      .subscribe(result => {
        if (result == GlobalConstants.popupYesValue) {
          this.removeItem();
        }
      });

  }

  removeItem() {
    this.productsService.Delete(this.id)
      .subscribe({
        next: (res: boolean) => {
          if (res) {
            this.popupService.openPopupAceptar(this.translateService.instant('shared.popup.delete_title'), this.translateService.instant('shared.popup.deleted'), "18.75rem", "");
            this.router.navigate(['/products/grid']);
          }
          else {
            this.popupService.openPopupAceptar(this.translateService.instant('shared.popup.delete_title'), this.translateService.instant('shared.popup.not_deleted'), "18.75rem", "");
          }
        },
        error: (err: HttpErrorResponse) => {
          this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
        }
      })
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }

  }

}