import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { IdDocumentTypeDto } from 'src/app/shared/models/iddocumenttypedto';
import { IdDocumentTypesService } from 'src/app/shared/services/iddocumenttypes.service';
import { Router } from '@angular/router';
import { TableColumn } from 'src/app/shared/interfaces/tablecolumn';

@Component({
  selector: 'app-iddocumenttypes-grid',
  templateUrl: './iddocumenttypes-grid.component.html',
  styleUrls: ['./iddocumenttypes-grid.component.css']
})

export class IdDocumentTypesGridComponent implements OnInit {

  headers : TableColumn[] = [
    { header : 'Id', field : 'id', visible: 'hidden', width: '0%' },
    { header : 'Name', field : 'name', visible: 'visible', width: '100%' },
  ];
  data: any[] = [];

  constructor(
    private iddocumenttypesservice: IdDocumentTypesService,
    private router: Router) {
  }

  ngOnInit(): void {
    this.loadiddocumenttypesData();
  }

  loadiddocumenttypesData() {
    this.iddocumenttypesservice.GetAll().subscribe({
      next: (res: Array<IdDocumentTypeDto>) => {
        if (res) {
          this.data = res;
        }
      },
      error: (err: HttpErrorResponse) => {
        console.log('Error al recuperar los Id Document Types', err);
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
  
}
