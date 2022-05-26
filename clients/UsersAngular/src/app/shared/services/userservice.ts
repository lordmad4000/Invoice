import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { JsonDocument } from '../models/jsondocument';
import { UserDto } from '../models/userdto';
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

    public Update(user: UserResponse): Observable<UserResponse> {

        const httpOptions = {
            headers : this.jwtService.GetHttpHeaderWithTokenFromSessionStorage()
        };

        const url = `${this.basePath}/api/User`;

        return this.httpClient.put<UserResponse>(url, user , httpOptions);
    }

    public Patch(jsonDocument: JsonDocument[], id : string): Observable<UserResponse> {
     
        const httpHeaders = this.jwtService.GetHttpHeaderWithTokenFromSessionStorage();

        const url = `${this.basePath}/api/User/PathUserById/${id}`;

        return this.httpClient.patch<UserResponse>(url, jsonDocument, { headers: httpHeaders } );
    }    

}