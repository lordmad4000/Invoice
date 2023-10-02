import { Component, OnInit } from '@angular/core';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { TableColumn } from 'src/app/shared/interfaces/tablecolumn';
import { UserResponse } from 'src/app/shared/models/userresponse';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-users-grid',
  templateUrl: './users-grid.component.html',
  styleUrls: ['./users-grid.component.css']
})
export class UsersGridComponent implements OnInit {
 
  private translate: any = (key: string) =>
    this.translateService.instant('users.' + key);

  headers : TableColumn[] = [
    { header : 'Id', field : 'id', visible: 'hidden', width: '0%' },
    { header : this.translate('fields.email'), field : 'email', visible: 'visible', width: '33%' },
    { header : this.translate('fields.firstname'), field : 'firstname', visible: 'visible', width: '33%' },
    { header : this.translate('fields.lastname'), field : 'lastname', visible: 'visible', width: '33%' },
  ];
  data: any[] = [];

  constructor(
    private location: Location,
    private userservice: UserService,
    private router: Router,
    private translateService: CustomTranslateService) {
  }

  ngOnInit(): void {
    this.loadGridData();
  }

  loadGridData() {
    this.userservice.GetAll().subscribe({
      next: (res: Array<UserResponse>) => {
        if (res) {
          this.data = res;
        }
      },
      error: (err: HttpErrorResponse) => {
        console.log('Error al recuperar los usuarios', err);
      }
    });
  }

  getRecord(event: any) {
    const row = event as UserResponse;
    this.router.navigate(['/users/view', `${row.id}`]);
  }

  addButtonClick() {
    this.router.navigate(['/users/new']);
  }

  backButtonClick() {
    this.location.back();
  }

}
