import { Component, OnDestroy, OnInit } from '@angular/core';
import { CustomTranslateService, ErrorService, IdDocumentTypesService } from 'src/app/shared/services';
import { CustomerDto } from 'src/app/shared/models/customerdto';
import { CustomerRegisterRequest } from 'src/app/shared/models/customerregisterrequest';
import { CustomersService } from 'src/app/shared/services/customers.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { IdDocumentTypeDto } from 'src/app/shared/models';
import { MatSelectChange } from '@angular/material/select';
import { Router } from '@angular/router';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-customers-new',
  templateUrl: './customers-new.component.html',
  styleUrls: ['./customers-new.component.css']
})

export class CustomersNewComponent implements OnInit, OnDestroy {

  private customer: CustomerRegisterRequest = new CustomerRegisterRequest();
  public idDocumentTypes: IdDocumentTypeDto[] = [];
  public selectedIdDocumentTypeId: string = '';
  public formCustomer: FormGroup;
  private subscription: Subscription | undefined;

  constructor(
    private customersService: CustomersService,
    private idDocumentTypeService: IdDocumentTypesService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    private router: Router,
    private snackBarService: SnackBarService,
    private translateService: CustomTranslateService) {

    this.formCustomer = this.formBuilder.group({
      name: [{ value: '', disabled: false }, Validators.required],
      value: [{ value: '', disabled: false }, Validators.required],
    });

    this.formCustomer = this.formBuilder.group({
      firstName: [{ value: '', disabled: false }],
      lastName: [{ value: '', disabled: false }],
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
    this.loadIdDocumentTypes();
  }

  private loadIdDocumentTypes() {
    this.idDocumentTypeService.GetAll().subscribe({
      next: (res: IdDocumentTypeDto[]) => {
        if (res) {
          this.idDocumentTypes = res;
          this.selectedIdDocumentTypeId = res[0].id;
        }
      },
      error: (err: HttpErrorResponse) => {
        this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
      }
    })
  }

  saveButtonClick() {
    this.customer.firstName = this.formCustomer.get("firstName")?.value;
    this.customer.lastName = this.formCustomer.get("lastName")?.value;
    this.customer.idDocumentTypeId = this.selectedIdDocumentTypeId;
    this.customer.idDocumentNumber = this.formCustomer.get("idDocumentNumber")?.value;
    this.customer.street = this.formCustomer.get("street")?.value;
    this.customer.city = this.formCustomer.get("city")?.value;
    this.customer.state = this.formCustomer.get("state")?.value;
    this.customer.country = this.formCustomer.get("country")?.value;
    this.customer.postalCode = this.formCustomer.get("postalCode")?.value;
    this.customer.phone = this.formCustomer.get("phone")?.value;
    this.customer.email = this.formCustomer.get("email")?.value;
    this.customersService.Post(this.customer).subscribe({
      next: (res: CustomerDto) => {
        if (res) {
          this.backButtonClick();
        }
      },
      error: (err: HttpErrorResponse) => {
        this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
      }
    });
  }

  onChangeIdDocumentTypes(event: MatSelectChange) {
    this.selectedIdDocumentTypeId = event.value;
  }

  backButtonClick() {
    this.router.navigate(['/customers/grid']);
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

}
