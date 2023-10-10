import { ActivatedRoute, Params, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { ErrorService, PopupService } from 'src/app/shared';
import { FormBuilder, FormGroup } from '@angular/forms';
import { GlobalConstants } from 'src/app/shared/const/global-constants';
import { HttpErrorResponse } from '@angular/common/http';
import { TaxRateDto } from 'src/app/shared/models/taxratedto';
import { TaxRatesService } from 'src/app/shared/services/taxrates.service';
import { Location } from  '@angular/common';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-taxrates-view',
  templateUrl: './taxrates-view.component.html',
  styleUrls: ['./taxrates-view.component.css']
})
export class TaxRatesViewComponent implements OnInit, OnDestroy {

  public formTaxRate: FormGroup;
  private subscription: Subscription | undefined;
  private id = "";

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private taxRatesService: TaxRatesService,
    private formBuilder: FormBuilder,
    private popupService: PopupService,
    private router: Router,
    private translateService: CustomTranslateService,
    private errorService: ErrorService,
    private snackBarService: SnackBarService) {

    this.formTaxRate = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      name: [{ value: '', disabled: true }],
      value: [{ value: '', disabled: true }],
    });
  }

  ngOnInit(): void {
    this.subscription = this.route.params.subscribe((params: Params): void => {
      this.id = params['id'];
      this.getTaxRate(this.id);
    });
  }

  private getTaxRate(id: string) {
    this.taxRatesService.Get(id)
      .subscribe({
        next: (res: TaxRateDto) => {
          if (res) {
            this.formTaxRate.patchValue(res);
          }
        },
        error: (err: HttpErrorResponse) => {
          this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
        }
      })
  }

  backButtonClick() {
    this.location.back();
  }

  editButtonClick() {
    this.router.navigate(['/taxrates/edit', `${this.id} `]);
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
    this.taxRatesService.Delete(this.id)
      .subscribe({
        next: (res: boolean) => {          
          if (res) {            
            this.popupService.openPopupAceptar(this.translateService.instant('shared.popup.delete_title'), this.translateService.instant('shared.popup.deleted'), "18.75rem", "");
            this.router.navigate(['/taxrates/grid']);
          }
          else{
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