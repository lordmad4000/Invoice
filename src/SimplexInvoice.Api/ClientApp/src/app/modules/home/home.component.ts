import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { UserResponse } from 'src/app/shared/models/userresponse';
import { UserService } from '../../shared/services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

  openSnackBar = (message: string) =>
    this.snackBar.open(message, '', { duration: 1 * 1000 });

    displayedColumns: string[] = [
      "email",
      "firstname",
      "lastname"
    ];
  
    start = 0;
    limit = 50;
    end: number = this.limit + this.start;
    selectedRowIndex = 0;
  
    users: UserResponse[] = [];
    dataSource = new MatTableDataSource<UserResponse>();
  
      constructor(private userservice: UserService,
                  private router: Router,
                  private snackBar: MatSnackBar
                  ) {}  
  
    ngOnInit(): void {
      this.loadUsersData();
    }
  
    loadUsersData() {
      this.userservice.GetLast(3).subscribe({
        next: (res: Array<UserResponse>) => {
          if (res) {
            this.users = res;
            this.dataSource = new MatTableDataSource(this.getTableData(this.start, this.end));
            this.updateIndex();
          }
        },
        error: (err : HttpErrorResponse) => {
          console.log('Error al recuperar los usuarios', err);
          this.openSnackBar('Error: ' + err);
        }
      });
    }
  
    getRecord(row: UserResponse) {
      this.router.navigate(['/users/view', `${row.id}`]);
    }
    
    getTableData(start: number, end: number) {
      return this.users.slice(start, end);
    }
  
    updateIndex() {
      this.start = this.end;
      this.end = this.limit + this.start;
    }

  }