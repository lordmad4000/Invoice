import { ActivatedRoute, Params, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { GlobalConstants } from 'src/app/shared/const/global-constants';
import { HttpErrorResponse } from '@angular/common/http';
import { IdDocumentTypeDto } from 'src/app/shared/models/iddocumenttypedto';
import { IdDocumentTypesService } from 'src/app/shared/services/iddocumenttypes.service';
import { Location } from  '@angular/common';
import { PopupService } from 'src/app/shared';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-iddocumenttypes-view',
  templateUrl: './iddocumenttypes-view.component.html',
  styleUrls: ['./iddocumenttypes-view.component.css']
})
export class IdDocumentTypesViewComponent implements OnInit, OnDestroy {

  public formiddocumenttype: FormGroup;
  private subscription: Subscription | undefined;
  private id = "";

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private iddocumenttypesService: IdDocumentTypesService,
    private formBuilder: FormBuilder,
    private popupService: PopupService,
    private router: Router) {

    this.formiddocumenttype = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      name: [{ value: '', disabled: true }],
    });
  }

  ngOnInit(): void {
    this.subscription = this.route.params.subscribe((params: Params): void => {
      this.id = params['id'];
      console.log(this.id);
      this.getiddocumenttype(this.id);
    });
  }

  private getiddocumenttype(id: string) {
    this.iddocumenttypesService.Get(id)
      .subscribe({
        next: (res: IdDocumentTypeDto) => {
          if (res) {
            this.formiddocumenttype.patchValue(res);
            console.log("");
          }
        },
        error: (err: HttpErrorResponse) => {
          console.log('Error al recuperar el usuario', err);
        }
      })
  }

  backButtonClick() {
    console.log("Back button.");
    this.location.back();
  }

  editButtonClick() {
    console.log("Edit button.");
    this.router.navigate(['/iddocumenttypes/edit', `${this.id} `]);
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
    this.iddocumenttypesService.Delete(this.id)
      .subscribe({
        next: (res: boolean) => {          
          if (res) {            
            this.popupService.openPopupAceptar("REMOVE", "Item removed.", "300px", "");
            this.router.navigate(['/iddocumenttypes/grid']);
          }
          else{
            this.popupService.openPopupAceptar("REMOVE", "Item not removed.", "300px", "");
          }          
        },
        error: (err: HttpErrorResponse) => {
          console.log('Error al recuperar el usuario', err);
        }
      })
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }

  }


}