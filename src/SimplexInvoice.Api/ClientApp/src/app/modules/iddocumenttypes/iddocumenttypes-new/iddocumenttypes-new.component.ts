import { Component, OnDestroy } from '@angular/core';
import { ErrorService } from 'src/app/shared/services';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { IdDocumentTypeDto } from 'src/app/shared/models/iddocumenttypedto';
import { IdDocumentTypeRegisterRequest } from 'src/app/shared/models/iddocumenttyperegisterrequest';
import { IdDocumentTypesService } from 'src/app/shared/services/iddocumenttypes.service';
import { Location } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-iddocumenttypes-new',
  templateUrl: './iddocumenttypes-new.component.html',
  styleUrls: ['./iddocumenttypes-new.component.css']
})

export class IdDocumentTypesNewComponent implements OnDestroy {

  private iddocumenttype: IdDocumentTypeRegisterRequest = new IdDocumentTypeRegisterRequest();
  formiddocumenttype: FormGroup;
  private subscription: Subscription | undefined;

  constructor(
    //private route: ActivatedRoute,
    private location: Location,
    private iddocumenttypesService: IdDocumentTypesService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    //private router: Router,
    private snackBar: MatSnackBar) {

    this.formiddocumenttype = this.formBuilder.group({
      name: [{ value: '', disabled: false }, Validators.required],
    });
  }

  openSnackBar(message: string) {
    this.snackBar.open(message, '', { duration: 1 * 1000 });
  }

  private getiddocumenttype(id: string) {
    this.iddocumenttypesService.Get(id).subscribe({
      next: (res: IdDocumentTypeDto) => {
        if (res) {
          this.iddocumenttype = res;
          this.formiddocumenttype.patchValue(res);
        }
      },
      error: (err : HttpErrorResponse) => {
        console.log('Error al recuperar el usuario', err);
      }
    })
  }

  saveButtonClick() {
    this.iddocumenttype.name = this.formiddocumenttype.get("name")?.value;
    this.iddocumenttypesService.Post(this.iddocumenttype).subscribe({
      next: (res: IdDocumentTypeDto) => {
        if (res) {
          this.location.back();
        }
      },
      error: (err: HttpErrorResponse) => {
        const errors = this.errorService.GetErrorsFromHttp(err);
        if (errors.length > 0) {
          errors.forEach(clientError => {
            console.log(clientError);
            this.openSnackBar(clientError);
          });
        }
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
