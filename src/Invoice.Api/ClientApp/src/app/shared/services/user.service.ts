import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { UserLoginResponse } from 'src/app/core/models/userloginresponse';
import { JsonDocument } from '../models/jsondocument';
import { UserDto } from '../models/userdto';
import { UserResponse } from '../models/userresponse';
import { JWTService } from './jwt.service';

@Injectable()
export class UserService {

    private baseUrl : string;

    constructor(protected httpClient: HttpClient,
                protected jwtService: JWTService,
                @Inject('BASE_URL') baseUrl: string) {
            this.baseUrl = baseUrl;
    }

    public GetAll(): Observable<Array<UserResponse>> {

        const httpHeaders = this.jwtService.GetHttpHeaderWithTokenFromSessionStorage();

        const url = `${this.baseUrl}api/User`;

        return this.httpClient.get<Array<UserResponse>>(url, { headers: httpHeaders, });
    }

    public Get(id: string): Observable<UserResponse> {

        const httpHeaders = this.jwtService.GetHttpHeaderWithTokenFromSessionStorage();

        const url = `${this.baseUrl}api/User/${encodeURIComponent(String(id))}`;

        return this.httpClient.get<UserResponse>(url, { headers: httpHeaders, });
    }

    public Post(user: UserDto): Observable<UserResponse> {

      const httpOptions = {
          headers : this.jwtService.GetHttpHeaderWithTokenFromSessionStorage()
      };

      const url = `${this.baseUrl}api/User`;

      return this.httpClient.post<UserResponse>(url, user , httpOptions);
    }

    public Update(user: UserResponse): Observable<UserResponse> {

        const httpOptions = {
            headers : this.jwtService.GetHttpHeaderWithTokenFromSessionStorage()
        };

        const url = `${this.baseUrl}api/User`;

        return this.httpClient.put<UserResponse>(url, user , httpOptions);
    }

    public Patch(jsonDocument: JsonDocument[], id : string): Observable<UserResponse> {

        const httpHeaders = this.jwtService.GetHttpHeaderWithTokenFromSessionStorage();

        const url = `${this.baseUrl}api/User/PathUserById/${id}`;

        return this.httpClient.patch<UserResponse>(url, jsonDocument, { headers: httpHeaders } );
    }

    public Delete(id : string): Observable<boolean> {

        const httpHeaders = this.jwtService.GetHttpHeaderWithTokenFromSessionStorage();

        const url = `${this.baseUrl}api/User/${encodeURIComponent(String(id))}`;

        return this.httpClient.delete<boolean>(url, { headers: httpHeaders } );
    }

}
