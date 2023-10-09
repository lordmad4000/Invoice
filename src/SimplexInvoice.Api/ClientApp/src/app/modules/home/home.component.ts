import { Component, OnInit } from '@angular/core';
import { ErrorService } from 'src/app/shared';
import { HttpErrorResponse } from '@angular/common/http';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { UserResponse } from 'src/app/shared/models/userresponse';
import { UserService } from '../../shared/services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

    displayedColumns: string[] = [
      "email",
      "firstName",
      "lastName"
    ];   
    users: UserResponse[] = [];
    dataSource = new MatTableDataSource<UserResponse>();
  
      constructor(private userservice: UserService,
                  private router: Router,
                  private snackBarService: SnackBarService,
                  private errorService: ErrorService
                  ) {}  
  
    ngOnInit(): void {
      this.loadUsersData();
    }
  
    loadUsersData() {
      this.userservice.GetLast(3).subscribe({
        next: (res: Array<UserResponse>) => {
          if (res) {
            this.users = res;
            this.dataSource =new MatTableDataSource(res);
          }
        },
        error: (err : HttpErrorResponse) => {
          this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));        }
      });
    }
  
    getRecord(row: UserResponse) {
      this.router.navigate(['/users/view', `${row.id}`]);
    }

  }