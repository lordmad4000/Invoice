import { Location } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { UserResponse } from 'src/app/shared/models/userresponse';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-users-grid',
  templateUrl: './users-grid.component.html',
  styleUrls: ['./users-grid.component.css']
})
export class UsersGridComponent implements OnInit {

  protected displayedColumns: string[] = [
    "email",
    "firstname",
    "lastname"
  ];

  private start : number = 0;
  private limit : number = 14;
  private end: number = this.limit + this.start;
  private max: number = 0;
  private users: UserResponse[] = [];
  protected dataSource = new MatTableDataSource<UserResponse>();

    constructor(private userservice: UserService,
                private router: Router,
                private location: Location) {
  }

  ngOnInit(): void {
    this.loadUsersData();  
  }

  loadUsersData() {
    this.userservice.GetAll().subscribe({
      next: (res: Array<UserResponse>) => {
        if (res) {
          this.users = res;
          this.max = this.users.length;
          this.dataSource = new MatTableDataSource(this.getTableData(this.start, this.end));
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

  updateIndex(position: number) {
    this.start = this.start + position;
    this.end = this.start + this.limit;
    if (this.start < 0){
      this.start = 0;
    }
    if (this.end > this.max){
      this.end = this.max;
    }
    const data = this.getTableData(this.start, this.end);
    this.dataSource.data = data;
  }

    addButtonClick() {
        console.log('Add button.');
        this.router.navigate(['/users/new']);
    }

    backButtonClick() {
        console.log('Back button.');
        this.location.back();
    }

    previousButtonClick() {
      this.updateIndex(-this.limit);
      }

    nextButtonClick() {
      this.updateIndex(this.limit);
    }
}
