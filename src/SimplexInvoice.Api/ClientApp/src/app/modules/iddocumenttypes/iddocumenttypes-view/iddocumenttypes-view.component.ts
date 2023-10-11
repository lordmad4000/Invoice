import { ActivatedRoute, Params, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { ErrorService, PopupService } from 'src/app/shared';
import { FormBuilder, FormGroup } from '@angular/forms';
import { GlobalConstants } from 'src/app/shared/const/global-constants';
import { HttpErrorResponse } from '@angular/common/http';
import { IdDocumentTypeDto } from 'src/app/shared/models/iddocumenttypedto';
import { IdDocumentTypesService } from 'src/app/shared/services/iddocumenttypes.service';
import { Location } from  '@angular/common';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-iddocumenttypes-view',
  templateUrl: './iddocumenttypes-view.component.html',
  styleUrls: ['./iddocumenttypes-view.component.css']
})
export class IdDocumentTypesViewComponent implements OnInit, OnDestroy {

  public formIdDocumentType: FormGroup;
  private subscription: Subscription | undefined;
  private id = "";

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private idDocumentTypesService: IdDocumentTypesService,
    private formBuilder: FormBuilder,
    private popupService: PopupService,
    private router: Router,
    private translateService: CustomTranslateService,
    private errorService: ErrorService,
    private snackBarService: SnackBarService) {

    this.formIdDocumentType = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      name: [{ value: '', disabled: true }],
    });
  }

  ngOnInit(): void {
    this.subscription = this.route.params.subscribe((params: Params): void => {
      this.id = params['id'];
      this.getIdDocumentType(this.id);
    });
  }

  private getIdDocumentType(id: string) {
    this.idDocumentTypesService.Get(id)
      .subscribe({
        next: (res: IdDocumentTypeDto) => {
          if (res) {
            this.formIdDocumentType.patchValue(res);
          }
        },
        error: (err: HttpErrorResponse) => {
          this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
        }
      })
  }

  backButtonClick() {
    this.location.back();
  }

  editButtonClick() {
    this.router.navigate(['/iddocumenttypes/edit', `${this.id} `]);
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
    this.idDocumentTypesService.Delete(this.id)
      .subscribe({
        next: (res: boolean) => {          
          if (res) {            
            this.popupService.openPopupAceptar(this.translateService.instant('shared.popup.delete_title'), this.translateService.instant('shared.popup.deleted'), "18.75rem", "");
            this.router.navigate(['/iddocumenttypes/grid']);
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