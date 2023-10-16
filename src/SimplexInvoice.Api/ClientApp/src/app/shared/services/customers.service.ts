import { CustomerDto } from '../models/customerdto';
import { CustomerRegisterRequest } from '../models/customerregisterrequest';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';

@Injectable()
export class CustomersService {

    private baseUrl = `${environment.API_BASE_URL}/api/Customers`;

    constructor(protected httpClient: HttpClient) {
    }

    public GetAll(): Observable<Array<CustomerDto>> {

        const url = `${this.baseUrl}/GetAll`;

        return this.httpClient.get<Array<CustomerDto>>(url);
    }

    public Get(id: string): Observable<CustomerDto> {

        const url = `${this.baseUrl}/GetById${encodeURIComponent(String(id))}`;

        return this.httpClient.get<CustomerDto>(url);
    }

    public Post(taxRateRegisterRequest: CustomerRegisterRequest): Observable<CustomerDto> {

        const url = `${this.baseUrl}/Register`;

        return this.httpClient.post<CustomerDto>(url, taxRateRegisterRequest);
    }

    public Update(taxRateDto: CustomerDto): Observable<CustomerDto> {

        const url = `${this.baseUrl}/Update`;

        return this.httpClient.put<CustomerDto>(url, taxRateDto);
    }

    public Delete(id: string): Observable<boolean> {

        const url = `${this.baseUrl}/Delete/${encodeURIComponent(String(id))}`;

        return this.httpClient.delete<boolean>(url);
    }

}
