import { ActivatedRoute, Params, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ErrorService } from 'src/app/shared/services/error.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { TaxRateDto } from 'src/app/shared/models/taxratedto';
import { TaxRatesService } from 'src/app/shared/services/taxrates.service';
import { Location } from '@angular/common';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-taxrates-edit',
  templateUrl: './taxrates-edit.component.html',
  styleUrls: ['./taxrates-edit.component.css']
})
export class TaxRatesEditComponent implements OnInit, OnDestroy {

  private taxRate: TaxRateDto = new TaxRateDto();
  public formTaxRate: FormGroup;
  private subscription: Subscription | undefined;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private taxRatesService: TaxRatesService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    private router: Router,
    private snackBarService: SnackBarService) {

    this.formTaxRate = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      name: [{ value: '', disabled: false }],
      value: [{ value: '', disabled: false }],
    });
  }

  ngOnInit(): void {
    this.subscription = this.route.params.subscribe((params: Params): void => {
      const id = params['id'];
      this.getTaxRate(id);
    });
  }

  private getTaxRate(id: string) {
    this.taxRatesService.Get(id).subscribe({
      next: (res: TaxRateDto) => {
        if (res) {
          this.taxRate = res;
          this.formTaxRate.patchValue(res);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
      }
    })
  }

  saveButtonClick() {
    this.taxRate.id = this.formTaxRate.get("id")?.value;
    this.taxRate.name = this.formTaxRate.get("name")?.value;
    this.taxRate.value = this.formTaxRate.get("value")?.value;
    this.taxRatesService.Update(this.taxRate).subscribe({
      next: (res: TaxRateDto) => {
        if (res) {
          this.router.navigate(['/taxrates/view', `${res.id}`]);
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