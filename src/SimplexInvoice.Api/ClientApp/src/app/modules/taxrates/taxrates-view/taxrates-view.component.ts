import { ActivatedRoute, Params, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { GlobalConstants } from 'src/app/shared/const/global-constants';
import { HttpErrorResponse } from '@angular/common/http';
import { Location } from  '@angular/common';
import { PopupService } from 'src/app/shared';
import { Subscription } from 'rxjs';
import { TaxRateDto } from 'src/app/shared/models/taxratedto';
import { TaxRatesService } from 'src/app/shared/services/taxrates.service';

@Component({
  selector: 'app-taxrates-view',
  templateUrl: './taxrates-view.component.html',
  styleUrls: ['./taxrates-view.component.css']
})
export class TaxRatesViewComponent implements OnInit, OnDestroy {

  public formtaxrate: FormGroup;
  private subscription: Subscription | undefined;
  private id = "";

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private taxratesService: TaxRatesService,
    private formBuilder: FormBuilder,
    private popupService: PopupService,
    private router: Router) {

    this.formtaxrate = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      name: [{ value: '', disabled: true }],
      value: [{value: 0, disabled: true}]
    });
  }

  ngOnInit(): void {
    this.subscription = this.route.params.subscribe((params: Params): void => {
      this.id = params['id'];
      console.log(this.id);
      this.gettaxrate(this.id);
    });
  }

  private gettaxrate(id: string) {
    this.taxratesService.Get(id)
      .subscribe({
        next: (res: TaxRateDto) => {
          if (res) {
            this.formtaxrate.patchValue(res);
          }
        },
        error: (err: HttpErrorResponse) => {
          console.log('Loading Tax Rate Error.', err);
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
    console.log("Delete button.");
      this.popupService
      .createConfirmPopup("Do you want to remove the item?")
      .afterClosed()
      .subscribe(result => {
        if (result == GlobalConstants.popupYesValue) {
          this.removeItem();
        }
      });

  }

  removeItem() {
    this.taxratesService.Delete(this.id)
      .subscribe({
        next: (res: boolean) => {          
          if (res) {            
            this.popupService.openPopupAceptar("REMOVE", "Item removed.", "300px", "");
            this.router.navigate(['/taxrates/grid']);
          }
          else{
            this.popupService.openPopupAceptar("REMOVE", "Item not removed.", "300px", "");
          }          
        },
        error: (err: HttpErrorResponse) => {
          console.log('Loading Tax Rate Error.', err);
        }
      })
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }

  }

}