import { Component, OnDestroy } from '@angular/core';
import { ErrorService } from 'src/app/shared/services';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { IdDocumentTypeDto } from 'src/app/shared/models/iddocumenttypedto';
import { IdDocumentTypeRegisterRequest } from 'src/app/shared/models/iddocumenttyperegisterrequest';
import { IdDocumentTypesService } from 'src/app/shared/services/iddocumenttypes.service';
import { Location } from '@angular/common';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-iddocumenttypes-new',
  templateUrl: './iddocumenttypes-new.component.html',
  styleUrls: ['./iddocumenttypes-new.component.css']
})

export class IdDocumentTypesNewComponent implements OnDestroy {

  private idDocumentType: IdDocumentTypeRegisterRequest = new IdDocumentTypeRegisterRequest();
  public formIdDocumentType: FormGroup;
  private subscription: Subscription | undefined;

  constructor(
    private location: Location,
    private idDocumentTypesService: IdDocumentTypesService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    private snackBarService: SnackBarService) {

    this.formIdDocumentType = this.formBuilder.group({
      name: [{ value: '', disabled: false }, Validators.required],
    });
  }

  saveButtonClick() {
    this.idDocumentType.name = this.formIdDocumentType.get("name")?.value;

    this.idDocumentTypesService.Post(this.idDocumentType).subscribe({
      next: (res: IdDocumentTypeDto) => {
        if (res) {
          this.location.back();
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
