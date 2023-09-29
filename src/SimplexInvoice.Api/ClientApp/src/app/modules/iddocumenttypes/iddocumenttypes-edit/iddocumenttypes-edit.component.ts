import { ActivatedRoute, Params, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ErrorService } from 'src/app/shared/services/error.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { IdDocumentTypeDto } from 'src/app/shared/models/iddocumenttypedto';
import { IdDocumentTypesService } from 'src/app/shared/services/iddocumenttypes.service';
import { Location } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-iddocumenttypes-edit',
  templateUrl: './iddocumenttypes-edit.component.html',
  styleUrls: ['./iddocumenttypes-edit.component.css']
})
export class IdDocumentTypesEditComponent implements OnInit, OnDestroy {

  private iddocumenttype: IdDocumentTypeDto = new IdDocumentTypeDto();
  public formiddocumenttype: FormGroup;
  private subscription: Subscription | undefined;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private iddocumenttypesService: IdDocumentTypesService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    private router: Router,
    private snackBar: MatSnackBar) {

    this.formiddocumenttype = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      name: [{ value: '', disabled: false }],
    });
  }

  ngOnInit(): void {
    this.subscription = this.route.params.subscribe((params: Params): void => {
      const id = params['id'];
      this.getiddocumenttype(id);
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
      error: (err: HttpErrorResponse) => {
        console.log('Error al recuperar el id document type', err);
        this.ShowErrors(err);
      }
    })
  }

  saveButtonClick() {
    this.iddocumenttype.id = this.formiddocumenttype.get("id")?.value;
    this.iddocumenttype.name = this.formiddocumenttype.get("name")?.value;
    this.iddocumenttypesService.Update(this.iddocumenttype).subscribe({
      next: (res: IdDocumentTypeDto) => {
        if (res) {
          this.router.navigate(['/iddocumenttypes/view', `${res.id}`]);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.ShowErrors(err);
      }
    });
  }

  backButtonClick() {
    this.location.back();
  }

  private ShowErrors(err: HttpErrorResponse)
  {
    const errors = this.errorService.GetErrorsFromHttp(err);
    if (errors.length > 0) {
      errors.forEach(clientError => {
        console.log(clientError);
        this.openSnackBar(clientError);
      });
    }
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }

  }

}