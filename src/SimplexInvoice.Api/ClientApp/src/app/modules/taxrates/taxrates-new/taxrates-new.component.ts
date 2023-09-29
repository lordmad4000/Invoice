import { Component, OnDestroy } from '@angular/core';
import { ErrorService } from 'src/app/shared/services';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Location } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { TaxRateDto } from 'src/app/shared/models/taxratedto';
import { TaxRateRegisterRequest } from 'src/app/shared/models/TaxRateRegisterRequest';
import { TaxRatesService } from 'src/app/shared/services/taxrates.service';

@Component({
  selector: 'app-taxrates-new',
  templateUrl: './taxrates-new.component.html',
  styleUrls: ['./taxrates-new.component.css']
})

export class TaxRatesNewComponent implements OnDestroy {
  
  public formTaxRate: FormGroup;
  private subscription: Subscription | undefined;

  constructor(
    private location: Location,
    private taxRatesService: TaxRatesService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    private snackBar: MatSnackBar) {

    this.formTaxRate = this.formBuilder.group({
      name: [{ value: '', disabled: false }, Validators.required],
      value: [{ value: '', disabled: false }, Validators.required],
    });
  }

  openSnackBar(message: string) {
    this.snackBar.open(message, '', { duration: 1 * 1000 });
  }
  
  validateFormData() : TaxRateRegisterRequest {
    const taxrate: TaxRateRegisterRequest = new TaxRateRegisterRequest();
    taxrate.name = this.formTaxRate.get("name")?.value;
    taxrate.value = this.formTaxRate.get("value")?.value;

    return taxrate;
  }

  saveButtonClick() {
    const taxrate = this.validateFormData();
    this.taxRatesService.Post(taxrate).subscribe({
      next: (res: TaxRateDto) => {
        if (res) {
          this.location.back();
        }
      },
      error: (err: HttpErrorResponse) => {
        const errors = this.errorService.GetErrorsFromHttp(err);
        if (errors.length > 0) {
          errors.forEach(clientError => {
            console.log(clientError);
            this.openSnackBar(clientError);
          });
        }
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
