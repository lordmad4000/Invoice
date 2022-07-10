import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { UserLoginResponse } from 'src/app/core/models/userloginresponse';

@Injectable()
export class AuthenticationService {

    private baseUrl : string;

    constructor(protected httpClient: HttpClient,
                @Inject('BASE_URL') baseUrl: string) {
            this.baseUrl = baseUrl;
    }

    public Login(username:string, password:string) : Observable<UserLoginResponse> {

      const httpHeaders = new HttpHeaders({
        'Content-Type': 'application/json'
      });

      const url = `${this.baseUrl}api/Authentication/Login?username=${username}&password=${password}`;

      return this.httpClient.get<UserLoginResponse>(url, { headers: httpHeaders } );
    }

}
