import { Component, OnInit } from '@angular/core';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { ErrorService } from 'src/app/shared';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
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
    { header : this.translate('forms.email'), field : 'email', visible: 'visible', width: '33%' },
    { header : this.translate('forms.password'), field : 'password', visible: 'hidden', width: '0%' },
    { header : this.translate('forms.firstname'), field : 'firstName', visible: 'visible', width: '33%' },
    { header : this.translate('forms.lastname'), field : 'lastName', visible: 'visible', width: '33%' },
  ];
  data: any[] = [];

  constructor(
    private userservice: UserService,
    private router: Router,
    private translateService: CustomTranslateService,
    private errorService: ErrorService,
    private snackBarService: SnackBarService) {
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
        this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
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
    this.router.navigate(['/home']);
  }

}
