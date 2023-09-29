import { ActivatedRoute, Params, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ErrorService } from 'src/app/shared/services/error.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Location } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { TaxRateDto } from 'src/app/shared/models/taxratedto';
import { TaxRatesService } from 'src/app/shared/services/taxrates.service';

@Component({
  selector: 'app-taxrates-edit',
  templateUrl: './taxrates-edit.component.html',
  styleUrls: ['./taxrates-edit.component.css']
})
export class TaxRatesEditComponent implements OnInit, OnDestroy {

  private taxRateDto: TaxRateDto = new TaxRateDto();
  public formTaxRate: FormGroup;
  private subscription: Subscription | undefined;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private taxRateService: TaxRatesService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    private router: Router,
    private snackBar: MatSnackBar) {

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

  openSnackBar(message: string) {
    this.snackBar.open(message, '', { duration: 1 * 1000 });
  }

  private getTaxRate(id: string) {
    this.taxRateService.Get(id).subscribe({
      next: (res: TaxRateDto) => {
        if (res) {
          this.taxRateDto = res;
          this.formTaxRate.patchValue(res);
        }
      },
      error: (err: HttpErrorResponse) => {
        console.log('Error al recuperar el id document type', err);
        this.ShowErrors(err);
      }
    })
  }

  validateFormData() : void {
    this.taxRateDto.id = this.formTaxRate.get("id")?.value;
    this.taxRateDto.name = this.formTaxRate.get("name")?.value;
    this.taxRateDto.value = this.formTaxRate.get("value")?.value;
  }

  saveButtonClick() {
    this.validateFormData();
    this.taxRateService.Update(this.taxRateDto).subscribe({
      next: (res: TaxRateDto) => {
        if (res) {
          this.router.navigate(['/taxrates/view', `${res.id}`]);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.ShowErrors(err);
      }
    });
  }

  backButtonClick() {
    this.location.back();
  }

  private ShowErrors(err: HttpErrorResponse)
  {
    const errors = this.errorService.GetErrorsFromHttp(err);
    if (errors.length > 0) {
      errors.forEach(clientError => {
        console.log(clientError);
        this.openSnackBar(clientError);
      });
    }
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }

  }

}