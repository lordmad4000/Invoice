import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JsonDocument } from '../models/jsondocument';
import { Observable } from 'rxjs/internal/Observable';
import { UserRegisterRequest } from '../models/userregisterrequest';
import { UserResponse } from '../models/userresponse';
import { UserUpdateRequest } from '../models/userupdaterequest';
import { environment } from 'src/environments/environment';

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

    public Post(userRegisterRequest: UserRegisterRequest): Observable<UserResponse> {

        const url = `${this.baseUrl}/Register`;

        return this.httpClient.post<UserResponse>(url, userRegisterRequest);
    }

    public Update(userUpdateRequest: UserUpdateRequest): Observable<UserResponse> {

        const url = `${this.baseUrl}/Update`;

        return this.httpClient.put<UserResponse>(url, userUpdateRequest);
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
