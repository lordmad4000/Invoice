import { ActivatedRoute, Params, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { CustomerDto } from 'src/app/shared/models/customerdto';
import { CustomersService } from 'src/app/shared/services/customers.service';
import { ErrorService, PopupService } from 'src/app/shared';
import { FormBuilder, FormGroup } from '@angular/forms';
import { GlobalConstants } from 'src/app/shared/const/global-constants';
import { HttpErrorResponse } from '@angular/common/http';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-customers-view',
  templateUrl: './customers-view.component.html',
  styleUrls: ['./customers-view.component.css']
})
export class CustomersViewComponent implements OnInit, OnDestroy {

  public formCustomer: FormGroup;
  private subscription: Subscription | undefined;
  private id = "";

  constructor(
    private route: ActivatedRoute,
    private customersService: CustomersService,
    private formBuilder: FormBuilder,
    private popupService: PopupService,
    private router: Router,
    private translateService: CustomTranslateService,
    private errorService: ErrorService,
    private snackBarService: SnackBarService) {

    this.formCustomer = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      firstName: [{ value: '', disabled: true }],
      lastName: [{ value: '', disabled: true }],
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
      this.id = params['id'];
      this.getCustomer(this.id);
    });
  }

  private getCustomer(id: string) {
    this.customersService.Get(id)
      .subscribe({
        next: (res: CustomerDto) => {
          if (res) {
            this.formCustomer.patchValue(res);
            this.formCustomer.patchValue( { idDocumentTypeName: res.idDocumentType.name });
          }
        },
        error: (err: HttpErrorResponse) => {
          this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
        }
      })
  }

  backButtonClick() {
    this.router.navigate(['/customers/grid']);
  }

  editButtonClick() {
    this.router.navigate(['/customers/edit', `${this.id} `]);
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
    this.customersService.Delete(this.id)
      .subscribe({
        next: (res: boolean) => {          
          if (res) {            
            this.popupService.openPopupAceptar(this.translateService.instant('shared.popup.delete_title'), this.translateService.instant('shared.popup.deleted'), "18.75rem", "");
            this.router.navigate(['/customers/grid']);
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