import { ActivatedRoute, Params, Router } from '@angular/router';
import { CompaniesService } from 'src/app/shared/services/companies.service';
import { CompanyDto } from 'src/app/shared/models/companydto';
import { CompanyUpdateRequest } from 'src/app/shared/models/companyupdaterequest';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ErrorService } from 'src/app/shared/services/error.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-companies-edit',
  templateUrl: './companies-edit.component.html',
  styleUrls: ['./companies-edit.component.css']
})
export class CompaniesEditComponent implements OnInit, OnDestroy {

  private company: CompanyUpdateRequest = new CompanyUpdateRequest();
  public formCompany: FormGroup;
  private subscription: Subscription | undefined;

  constructor(
    private route: ActivatedRoute,
    private companiesService: CompaniesService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    private router: Router,
    private snackBarService: SnackBarService) {

    this.formCompany = this.formBuilder.group({
      name: [{ value: '', disabled: false }],
      idDocumentTypeId: [{ value: '', disabled: false }],
      idDocumentTypeName: [{ value: '', disabled: true }],
      idDocumentNumber: [{ value: '', disabled: false }],
      street: [{ value: '', disabled: false }],
      city: [{ value: '', disabled: false }],
      state: [{ value: '', disabled: false }],
      country: [{ value: '', disabled: false }],
      postalCode: [{ value: '', disabled: false }],
      phone: [{ value: '', disabled: false }],
      email: [{ value: '', disabled: false }],
    });
  }

  ngOnInit(): void {
    this.subscription = this.route.params.subscribe((params: Params): void => {
      const id = params['id'];
      this.getCompany(id);
    });
  }

  private getCompany(id: string) {
    this.companiesService.Get().subscribe({
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

  saveButtonClick() {
    this.company.name = this.formCompany.get("name")?.value;
    this.company.idDocumentTypeId = this.formCompany.get("idDocumentTypeId")?.value;
    this.company.idDocumentNumber = this.formCompany.get("idDocumentNumber")?.value;
    this.company.street = this.formCompany.get("street")?.value;
    this.company.city = this.formCompany.get("city")?.value;
    this.company.state = this.formCompany.get("state")?.value;
    this.company.country = this.formCompany.get("country")?.value;
    this.company.postalCode = this.formCompany.get("postalCode")?.value;
    this.company.phone = this.formCompany.get("phone")?.value;
    this.company.email = this.formCompany.get("email")?.value;   
    this.companiesService.Update(this.company).subscribe({
      next: (res: CompanyDto) => {
        if (res) {
          this.router.navigate(['/companies/view']);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
      }
    });
  }

  backButtonClick() {
    this.router.navigate(['/companies/view']);
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }

  }

}