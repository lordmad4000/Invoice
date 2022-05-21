import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { UserResponse } from 'src/app/shared/models/userresponse';
import { Userservice } from 'src/app/shared/services/userservice';

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
    // "firstname",
    // "lastname",
    "email"
  ];

  dataSource: UserResponse[] = [];
  //dataSource = new MatTableDataSource<UserResponse>(ELEMENT_DATA);
  selection = new SelectionModel<UserResponse>(true, []);

  constructor(private userservice: Userservice) {
    // this.dataSource = [
    //   { id: "1", userName: "pepe", firstName: "Pepe", lastName: "García", email: "pepe@gmail.com" },
    //   { id: "2", userName: "felix", firstName: "Felix", lastName: "López", email: "felix@gmail.com" },
    //   { id: "3", userName: "antonio", firstName: "Antonio", lastName: "Puertas", email: "antonio@gmail.com" },
    //   { id: "4", userName: "julian", firstName: "Julian", lastName: "Herrero", email: "julian@gmail.com" },
    //   { id: "5", userName: "ramiro", firstName: "Ramiro", lastName: "Sánchez", email: "ramiro@gmail.com" },
    //   { id: "6", userName: "sebastian", firstName: "Sebastian", lastName: "El Cano", email: "sebastian@gmail.com" },
    // ];
  }

  ngOnInit(): void {

    this.dataSource = this.userservice.GetAll();

  }

  selectHandler(row: UserResponse) {
    this.selection.toggle(row);
  }

  onChange(typeValue: number) {
    this.selection.clear();
  }
}

const ELEMENT_DATA: UserResponse[] = [
  { id: "1", userName: "pepe", firstName: "Pepe", lastName: "García", email: "pepe@gmail.com" },
  { id: "2", userName: "felix", firstName: "Felix", lastName: "López", email: "felix@gmail.com" },
  { id: "3", userName: "antonio", firstName: "Antonio", lastName: "Puertas", email: "antonio@gmail.com" },
  { id: "4", userName: "julian", firstName: "Julian", lastName: "Herrero", email: "julian@gmail.com" },
  { id: "5", userName: "ramiro", firstName: "Ramiro", lastName: "Sánchez", email: "ramiro@gmail.com" },
  { id: "6", userName: "sebastian", firstName: "Sebastian", lastName: "El Cano", email: "sebastian@gmail.com" },
  { id: "7", userName: "felix", firstName: "Felix", lastName: "López", email: "felix@gmail.com" },
  { id: "8", userName: "antonio", firstName: "Antonio", lastName: "Puertas", email: "antonio@gmail.com" },
  { id: "9", userName: "julian", firstName: "Julian", lastName: "Herrero", email: "julian@gmail.com" },
  { id: "10", userName: "ramiro", firstName: "Ramiro", lastName: "Sánchez", email: "ramiro@gmail.com" },
  { id: "11", userName: "sebastian", firstName: "Sebastian", lastName: "El Cano", email: "sebastian@gmail.com" },
];