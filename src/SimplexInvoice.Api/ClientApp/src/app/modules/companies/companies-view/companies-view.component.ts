import { ActivatedRoute, Params, Router } from '@angular/router';
import { CompaniesService } from 'src/app/shared/services/companies.service';
import { CompanyDto } from 'src/app/shared/models/companydto';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ErrorService } from 'src/app/shared';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-companies-view',
  templateUrl: './companies-view.component.html',
  styleUrls: ['./companies-view.component.css']
})
export class CompaniesViewComponent implements OnInit, OnDestroy {

  public formCompany: FormGroup;
  private subscription: Subscription | undefined;

  constructor(
    private route: ActivatedRoute,
    private companiesService: CompaniesService,
    private formBuilder: FormBuilder,
    private router: Router,
    private errorService: ErrorService,
    private snackBarService: SnackBarService) {

    this.formCompany = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      name: [{ value: '', disabled: true }],
      idDocumentTypeId: [{ value: '', disabled: true }],
      idDocumentTypeName: [{ value: '', disabled: true }],
      idDocumentNumber: [{ value: '', disabled: true }],
      street: [{ value: '', disabled: true }],
      city: [{ value: '', disabled: true }],
      state: [{ value: '', disabled: true }],
      country: [{ value: '', disabled: true }],
      postalCode: [{ value: '', disabled: true }],
      phone: [{ value: '', disabled: true }],
      email: [{ value: '', disabled: true }],
    });
  }

  ngOnInit(): void {
    this.subscription = this.route.params.subscribe((params: Params): void => {
      this.getCompany();
    });
  }

  private getCompany() {
    this.companiesService.Get()
      .subscribe({
        next: (res: CompanyDto) => {
          if (res) {
            this.formCompany.patchValue(res);
            this.formCompany.patchValue( { idDocumentTypeName: res.idDocumentType.name });
          }
        },
        error: (err: HttpErrorResponse) => {
          this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
        }
      })
  }

  backButtonClick() {
    this.router.navigate(['/home']);
  }

  editButtonClick() {
    this.router.navigate(['/companies/edit']);
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }

  }

}