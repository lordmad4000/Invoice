import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserResponse } from '../models/userresponse';

@Injectable()
export class Userservice {

    users: UserResponse[] = [];

    constructor(private httpClient: HttpClient) {
    }

    public GetAll(): UserResponse[] {
        const auth_token = sessionStorage.getItem('token');

        const httpHeaders = new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + auth_token
        });
        this.httpClient
            .get<UserResponse[]>('http://localhost:21440/api/User', { headers: httpHeaders })
            .subscribe({
                next: (res: any) => {
                    this.users = res;
                },
                error: (err) => {
                    console.log('Error al recuperar los usuarios', err);
                }
            });

        return this.users;
    }
}