import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { UserLoginResponse } from 'src/app/core/models/userloginresponse';
import { JsonDocument } from '../models/jsondocument';
import { UserDto } from '../models/userdto';
import { UserResponse } from '../models/userresponse';
import { JWTService } from '../../core/services/jwt.service';

@Injectable()
export class UserService {

    private baseUrl: string;

    constructor(protected httpClient: HttpClient,
        @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    public GetAll(): Observable<Array<UserResponse>> {

        const url = `${this.baseUrl}api/User`;

        return this.httpClient.get<Array<UserResponse>>(url);
    }

    public Get(id: string): Observable<UserResponse> {

        const url = `${this.baseUrl}api/User/${encodeURIComponent(String(id))}`;

        return this.httpClient.get<UserResponse>(url);
    }

    public Post(user: UserDto): Observable<UserResponse> {

        const url = `${this.baseUrl}api/User`;

        return this.httpClient.post<UserResponse>(url, user);
    }

    public Update(user: UserResponse): Observable<UserResponse> {

        const url = `${this.baseUrl}api/User`;

        return this.httpClient.put<UserResponse>(url, user);
    }

    public Patch(jsonDocument: JsonDocument[], id: string): Observable<UserResponse> {

        const url = `${this.baseUrl}api/User/PathUserById/${id}`;

        return this.httpClient.patch<UserResponse>(url, jsonDocument);
    }

    public Delete(id: string): Observable<boolean> {

        const url = `${this.baseUrl}api/User/${encodeURIComponent(String(id))}`;

        return this.httpClient.delete<boolean>(url);
    }

}
