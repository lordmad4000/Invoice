import { CompanyDto } from '../models/companydto';
import { CompanyUpdateRequest } from '../models/companyupdaterequest';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';

@Injectable()
export class CompaniesService {

    private baseUrl = `${environment.API_BASE_URL}/api/Companies`;

    constructor(protected httpClient: HttpClient) {
    }

    public Get(): Observable<CompanyDto> {

        const url = `${this.baseUrl}/Get`;

        return this.httpClient.get<CompanyDto>(url);
    }

    public Update(companyUpdateRequest: CompanyUpdateRequest): Observable<CompanyDto> {

        const url = `${this.baseUrl}/Update`;

        return this.httpClient.put<CompanyDto>(url, companyUpdateRequest);
    }

}
