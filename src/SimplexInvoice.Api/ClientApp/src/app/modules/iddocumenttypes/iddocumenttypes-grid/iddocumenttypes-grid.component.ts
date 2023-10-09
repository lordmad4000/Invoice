import { Component, OnInit } from '@angular/core';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { ErrorService } from 'src/app/shared';
import { HttpErrorResponse } from '@angular/common/http';
import { Location } from '@angular/common';
import { IdDocumentTypeDto } from 'src/app/shared/models/iddocumenttypedto';
import { IdDocumentTypesService } from 'src/app/shared/services/iddocumenttypes.service';
import { Router } from '@angular/router';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { TableColumn } from 'src/app/shared/interfaces/tablecolumn';

@Component({
  selector: 'app-iddocumenttypes-grid',
  templateUrl: './iddocumenttypes-grid.component.html',
  styleUrls: ['./iddocumenttypes-grid.component.css']
})

export class IdDocumentTypesGridComponent implements OnInit {

  private translate: any = (key: string) =>
    this.translateService.instant('iddocumenttypes.' + key);


  headers: TableColumn[] = [
    { header: 'Id', field: 'id', visible: 'hidden', width: '0%' },
    { header: this.translate('forms.name'), field: 'name', visible: 'visible', width: '100%' },
  ];
  public data: any[] = [];

  constructor(
    private location: Location,
    private idDocumentTypesService: IdDocumentTypesService,
    private router: Router,
    private translateService: CustomTranslateService,
    private errorService: ErrorService,
    private snackBarService: SnackBarService) {
  }

  ngOnInit(): void {
    this.loadGridData();
  }

  loadGridData() {
    this.idDocumentTypesService.GetAll().subscribe({
      next: (res: Array<IdDocumentTypeDto>) => {
        if (res) {
          this.data = res;
        }
      },
      error: (err: HttpErrorResponse) => {
        this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
      }
    });
  }

  getRecord(event: any) {
    const row = event as IdDocumentTypeDto;
    this.router.navigate(['/iddocumenttypes/view', `${row.id}`]);
  }

  addButtonClick() {
    this.router.navigate(['/iddocumenttypes/new']);
  }

  backButtonClick() {
    this.location.back();
  }

}
