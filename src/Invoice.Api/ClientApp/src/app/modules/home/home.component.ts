import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { Router } from '@angular/router';
import { UserResponse } from 'src/app/shared/models/userresponse';
import { UserService } from 'src/app/shared/services/user.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

    displayedColumns: string[] = [
      "email",
      "firstname",
      "lastname"
    ];
  
    start: number = 0;
    limit: number = 50;
    end: number = this.limit + this.start;
    selectedRowIndex: number = 0;
  
    users: UserResponse[] = [];
    dataSource = new MatTableDataSource<UserResponse>();
  
      constructor(private userservice: UserService,
                  private router: Router) {
    }
  
    ngOnInit(): void {
      this.loadUsersData();
    }
  
    loadUsersData() {
      this.userservice.GetLast(3).subscribe({
        next: (res: any) => {
          const data = res;
          if (data) {
            this.users = data;
            this.dataSource = new MatTableDataSource(this.getTableData(this.start, this.end));
            this.updateIndex();
          }
        },
        error: (err : HttpErrorResponse) => {
          console.log('Error al recuperar los usuarios', err);
        }
      });
    }
  
    getRecord(row: UserResponse) {
      console.log(row);
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