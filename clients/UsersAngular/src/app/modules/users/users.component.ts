import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserResponse } from 'src/app/shared/models/userresponse';
import { UserService } from 'src/app/shared/services/userservice';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})

export class UsersComponent implements OnInit {

  displayedColumns: string[] = [
    "select",
    "username",
    "name",
    "email"
  ];

  dataSource: UserResponse[] = [];
  selection = new SelectionModel<UserResponse>(true, []);

  constructor(private userservice: UserService, private router: Router) {
  }

  ngOnInit(): void {

    this.userservice.GetAll().subscribe({
      next: (res: any) => {
        const data = res;
        if (data) {
          this.dataSource = data;
        }
      },
      error: (err) => {
        console.log('Error al recuperar los usuarios', err);
      }
    });

  }

  selectHandler(row: UserResponse) {
    this.selection.toggle(row);
  }

  onChange(typeValue: number) {
    this.selection.clear();
  }

  editUserButtonClick($event: any) {
    console.log("Edit button click.")
    if (this.selection.hasValue()) {
      const id = this.selection.selected[0].id;
      this.router.navigate(['/users/edit', `${id}`]);
    }
  }

}