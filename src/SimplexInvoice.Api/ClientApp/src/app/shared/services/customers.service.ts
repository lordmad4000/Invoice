import { BasicCustomer } from '../models/basiccustomer';
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

    public Post(customerRegisterRequest: CustomerRegisterRequest): Observable<CustomerDto> {

        const url = `${this.baseUrl}/Register`;

        return this.httpClient.post<CustomerDto>(url, customerRegisterRequest);
    }

    public Update(customerDto: CustomerDto): Observable<CustomerDto> {

        const url = `${this.baseUrl}/Update`;

        return this.httpClient.put<CustomerDto>(url, customerDto);
    }

    public Delete(id: string): Observable<boolean> {

        const url = `${this.baseUrl}/Delete/${encodeURIComponent(String(id))}`;

        return this.httpClient.delete<boolean>(url);
    }

    public GetBasicCustomersContainsFullName(fullName: string): Observable<Array<BasicCustomer>> {

        const url = `${this.baseUrl}/GetBasicCustomersContainsFullName${encodeURIComponent(String(fullName))}`;

        return this.httpClient.get<Array<BasicCustomer>>(url);
    }

    public GetBasicCustomersContainsEmail(email: string): Observable<Array<BasicCustomer>> {

        const url = `${this.baseUrl}/GetBasicCustomersContainsEmail${encodeURIComponent(String(email))}`;

        return this.httpClient.get<Array<BasicCustomer>>(url);

    }

    public GetBasicCustomersContainsIdDocumentNumber(idDocumentNumber: string): Observable<Array<BasicCustomer>> {

        const url = `${this.baseUrl}/GetBasicCustomersContainsIdDocumentNumber${encodeURIComponent(String(idDocumentNumber))}`;

        return this.httpClient.get<Array<BasicCustomer>>(url);
    }

}
