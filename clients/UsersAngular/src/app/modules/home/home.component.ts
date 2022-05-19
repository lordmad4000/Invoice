import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { User } from 'src/app/shared/models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

  users: User[] = [];

  constructor(private httpClient: HttpClient,
    private router: Router) {
  }

  ngOnInit(): void {
    const auth_token = sessionStorage.getItem('token');

    const httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + auth_token
    });

    this.httpClient
      .get<User[]>('http://localhost:21440/api/User', { headers: httpHeaders, })
      .subscribe({
        next: (res: any) => {
          this.users = res;
        },
        error: (err) => {
          console.log('Error al recuperar los usuarios', err);
        }
      });
  }

}
