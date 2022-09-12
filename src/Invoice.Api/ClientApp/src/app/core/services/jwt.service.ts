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

    public GetTokenFromSessionStorage(): string {
        return sessionStorage.getItem('token');
    }

    public GetTokenExpiricyTime() : string {
        const auth_token = sessionStorage.getItem('token');
        const expiry  = (JSON.parse(atob(auth_token.split('.')[1]))).exp;        
        const currentDate = new Date();
        const expiryDate = new Date(expiry * 1000);

        const leftTime = Math.floor((expiryDate.getTime() - currentDate.getTime()) / 1000);

        return leftTime.toString();
    }

}