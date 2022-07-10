import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class JWTService {

    constructor() {
    }

    public GetHttpHeaderWithTokenFromSessionStorage(): HttpHeaders {
        const auth_token = sessionStorage.getItem('token');

        return new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + auth_token
        });
    }

}