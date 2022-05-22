import { HttpClient, HttpEvent, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { observeNotification } from 'rxjs/internal/Notification';
import { Observable } from 'rxjs/internal/Observable';
import { UserResponse } from '../models/userresponse';
import { JWTService } from './jwtservice';

@Injectable()
export class UserService {

    protected basePath = 'http://localhost:21440';

    constructor(protected httpClient: HttpClient,
        protected jwtService: JWTService) {
    }

    public GetAll(): Observable<Array<UserResponse>> {

        const httpHeaders = this.jwtService.GetHttpHeaderWithTokenFromSessionStorage();

        const url = `${this.basePath}/api/User`;

        return this.httpClient.get<Array<UserResponse>>(url, { headers: httpHeaders, });
    }

    public Get(id: string): Observable<UserResponse> {

        const httpHeaders = this.jwtService.GetHttpHeaderWithTokenFromSessionStorage();

        const url = `${this.basePath}/api/User/${encodeURIComponent(String(id))}`;

        return this.httpClient.get<UserResponse>(url, { headers: httpHeaders, });
    }

}