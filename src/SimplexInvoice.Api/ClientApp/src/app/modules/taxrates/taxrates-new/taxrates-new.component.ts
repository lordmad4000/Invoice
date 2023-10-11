import { Component, OnDestroy } from '@angular/core';
import { ErrorService } from 'src/app/shared/services';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { TaxRateDto } from 'src/app/shared/models/taxratedto';
import { TaxRateRegisterRequest } from 'src/app/shared/models/taxrateregisterrequest';
import { TaxRatesService } from 'src/app/shared/services/taxrates.service';
import { Location } from '@angular/common';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-taxrates-new',
  templateUrl: './taxrates-new.component.html',
  styleUrls: ['./taxrates-new.component.css']
})

export class TaxRatesNewComponent implements OnDestroy {

  private taxRate: TaxRateRegisterRequest = new TaxRateRegisterRequest();
  public formTaxRate: FormGroup;
  private subscription: Subscription | undefined;

  constructor(
    private location: Location,
    private taxRatesService: TaxRatesService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    private snackBarService: SnackBarService) {

    this.formTaxRate = this.formBuilder.group({
      name: [{ value: '', disabled: false }, Validators.required],
      value: [{ value: '', disabled: false }, Validators.required],
    });
  }

  saveButtonClick() {
    this.taxRate.name = this.formTaxRate.get("name")?.value;
    this.taxRate.value = this.formTaxRate.get("value")?.value;

    this.taxRatesService.Post(this.taxRate).subscribe({
      next: (res: TaxRateDto) => {
        if (res) {
          this.location.back();
        }
      },
      error: (err: HttpErrorResponse) => {
        this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
      }
    });
  }

  backButtonClick() {
    this.location.back();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

}
