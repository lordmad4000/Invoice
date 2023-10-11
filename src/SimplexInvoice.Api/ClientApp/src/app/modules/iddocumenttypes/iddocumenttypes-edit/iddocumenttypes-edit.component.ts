import { ActivatedRoute, Params, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ErrorService } from 'src/app/shared/services/error.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { IdDocumentTypeDto } from 'src/app/shared/models/iddocumenttypedto';
import { IdDocumentTypesService } from 'src/app/shared/services/iddocumenttypes.service';
import { Location } from '@angular/common';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-iddocumenttypes-edit',
  templateUrl: './iddocumenttypes-edit.component.html',
  styleUrls: ['./iddocumenttypes-edit.component.css']
})
export class IdDocumentTypesEditComponent implements OnInit, OnDestroy {

  private idDocumentType: IdDocumentTypeDto = new IdDocumentTypeDto();
  public formIdDocumentType: FormGroup;
  private subscription: Subscription | undefined;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private idDocumentTypesService: IdDocumentTypesService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    private router: Router,
    private snackBarService: SnackBarService) {

    this.formIdDocumentType = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      name: [{ value: '', disabled: false }],
    });
  }

  ngOnInit(): void {
    this.subscription = this.route.params.subscribe((params: Params): void => {
      const id = params['id'];
      this.getIdDocumentType(id);
    });
  }

  private getIdDocumentType(id: string) {
    this.idDocumentTypesService.Get(id).subscribe({
      next: (res: IdDocumentTypeDto) => {
        if (res) {
          this.idDocumentType = res;
          this.formIdDocumentType.patchValue(res);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
      }
    })
  }

  saveButtonClick() {
    this.idDocumentType.id = this.formIdDocumentType.get("id")?.value;
    this.idDocumentType.name = this.formIdDocumentType.get("name")?.value;
    this.idDocumentTypesService.Update(this.idDocumentType).subscribe({
      next: (res: IdDocumentTypeDto) => {
        if (res) {
          this.router.navigate(['/iddocumenttypes/view', `${res.id}`]);
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