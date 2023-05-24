import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { UserResponse } from '../models/userresponse';
import { UserRegisterRequest } from '../models/userregisterrequest';
import { UserUpdateRequest } from '../models/userupdaterequest';
import { JsonDocument } from '../models/jsondocument';

@Injectable()
export class UserService {

    private baseUrl = `${environment.API_BASE_URL}/api/Users`;

    constructor(protected httpClient: HttpClient) {
    }

    public GetAll(): Observable<Array<UserResponse>> {

        const url = `${this.baseUrl}/GetAll`;

        return this.httpClient.get<Array<UserResponse>>(url);
    }

    public GetLast(count: number): Observable<Array<UserResponse>> {

        const url = `${this.baseUrl}/GetLast${encodeURIComponent(String(count))}`;

        return this.httpClient.get<Array<UserResponse>>(url);
    }

    public Get(id: string): Observable<UserResponse> {

        const url = `${this.baseUrl}/GetById${encodeURIComponent(String(id))}`;

        return this.httpClient.get<UserResponse>(url);
    }

    public Post(user: UserRegisterRequest): Observable<UserResponse> {

        const url = `${this.baseUrl}/Register`;

        return this.httpClient.post<UserResponse>(url, user);
    }

    public Update(user: UserUpdateRequest): Observable<UserResponse> {

        const url = `${this.baseUrl}/Update`;

        return this.httpClient.put<UserResponse>(url, user);
    }

    public Patch(jsonDocument: JsonDocument[], id: string): Observable<UserResponse> {

        const url = `${this.baseUrl}/PatchReplaceById/${id}`;

        return this.httpClient.patch<UserResponse>(url, jsonDocument);
    }

    public Delete(id: string): Observable<boolean> {

        const url = `${this.baseUrl}/Delete/${encodeURIComponent(String(id))}`;

        return this.httpClient.delete<boolean>(url);
    }

}
