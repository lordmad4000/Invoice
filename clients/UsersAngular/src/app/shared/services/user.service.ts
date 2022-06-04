import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { UserLoginResponse } from 'src/app/core/models/userloginresponse';
import { JsonDocument } from '../models/jsondocument';
import { UserDto } from '../models/userdto';
import { UserResponse } from '../models/userresponse';
import { JWTService } from './jwt.service';

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

    public Post(user: UserDto): Observable<UserResponse> {

      const httpOptions = {
          headers : this.jwtService.GetHttpHeaderWithTokenFromSessionStorage()
      };

      const url = `${this.basePath}/api/User`;

      return this.httpClient.post<UserResponse>(url, user , httpOptions);
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

    public Delete(id : string): Observable<boolean> {

        const httpHeaders = this.jwtService.GetHttpHeaderWithTokenFromSessionStorage();

        const url = `${this.basePath}/api/User/${encodeURIComponent(String(id))}`;

        return this.httpClient.delete<boolean>(url, { headers: httpHeaders } );
    }

    public Login(username:string, password:string) : Observable<UserLoginResponse> {

      const url = `${this.basePath}/api/User/Login?username=${username}&password=${password}`;

      //curl -X GET "http://localhost:21440/api/User/Login?username=lordmad&password=12345678" -H "accept: */*"

      return this.httpClient.get<UserLoginResponse>(url);
    }

}
