import { ActivatedRoute, Params, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { CustomerDto } from 'src/app/shared/models/customerdto';
import { CustomersService } from 'src/app/shared/services/customers.service';
import { ErrorService } from 'src/app/shared/services/error.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { IdDocumentTypeDto } from 'src/app/shared/models';
import { IdDocumentTypesService } from 'src/app/shared';
import { MatSelectChange } from '@angular/material/select';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-customers-edit',
  templateUrl: './customers-edit.component.html',
  styleUrls: ['./customers-edit.component.css']
})
export class CustomersEditComponent implements OnInit, OnDestroy {

  private customer: CustomerDto = new CustomerDto();
  public idDocumentTypes: IdDocumentTypeDto[] = [];
  public selectedIdDocumentTypeId: string = '';  
  public formCustomer: FormGroup;
  private subscription: Subscription | undefined;

  constructor(
    private route: ActivatedRoute,
    private customersService: CustomersService,
    private idDocumentTypeService: IdDocumentTypesService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    private router: Router,
    private snackBarService: SnackBarService) {

    this.formCustomer = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
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
    this.subscription = this.route.params.subscribe((params: Params): void => {
      const id = params['id'];
      this.getCustomer(id);
    });
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

  private getCustomer(id: string) {
    this.customersService.Get(id).subscribe({
      next: (res: CustomerDto) => {
        if (res) {
          this.customer = res;
          this.formCustomer.patchValue(res);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
      }
    })
  }

  saveButtonClick() {
    this.customer.id = this.formCustomer.get("id")?.value;
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
    this.customersService.Update(this.customer).subscribe({
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
    this.router.navigate(['/customers/view', `${this.customer.id}`]);
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }

  }

}