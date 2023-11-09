import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { InvoiceDto } from '../models/invoicedto';
import { InvoiceRegisterRequest } from '../models/invoiceregisterrequest';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';

@Injectable()
export class InvoicesService {

    private baseUrl = `${environment.API_BASE_URL}/api/Invoices`;

    constructor(protected httpClient: HttpClient) {
    }

    public GetAll(): Observable<Array<InvoiceDto>> {

        const url = `${this.baseUrl}/GetAll`;

        return this.httpClient.get<Array<InvoiceDto>>(url);
    }

    public Get(id: string): Observable<InvoiceDto> {

        const url = `${this.baseUrl}/GetById${encodeURIComponent(String(id))}`;

        return this.httpClient.get<InvoiceDto>(url);
    }

    public Post(invoiceRegisterRequest: InvoiceRegisterRequest): Observable<InvoiceDto> {

        const url = `${this.baseUrl}/Register`;

        return this.httpClient.post<InvoiceDto>(url, invoiceRegisterRequest);
    }
    
}
