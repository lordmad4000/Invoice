import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { UserLoginResponse } from '../models/userloginresponse';
import { environment } from '../../../environments/environment';

@Injectable()
export class AuthenticationService {

    constructor(protected httpClient: HttpClient) {
    }

    public Login(email:string, password:string) : Observable<UserLoginResponse> {

      const httpHeaders = new HttpHeaders({
        'Content-Type': 'application/json'
      });
      
      const url = `${environment.API_BASE_URL}/api/Authentication/Login?email=${email}&password=${password}`;

      return this.httpClient.get<UserLoginResponse>(url, { headers: httpHeaders } );
    }

}
