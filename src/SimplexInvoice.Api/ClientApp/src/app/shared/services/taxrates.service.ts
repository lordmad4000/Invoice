import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { TaxRateDto } from '../models/taxratedto';
import { TaxRateRegisterRequest } from '../models/taxrateregisterrequest';
import { environment } from 'src/environments/environment';

@Injectable()
export class TaxRatesService {

    private baseUrl = `${environment.API_BASE_URL}/api/TaxRates`;

    constructor(protected httpClient: HttpClient) {
    }

    public GetAll(): Observable<Array<TaxRateDto>> {

        const url = `${this.baseUrl}/GetAll`;

        return this.httpClient.get<Array<TaxRateDto>>(url);
    }

    public Get(id: string): Observable<TaxRateDto> {

        const url = `${this.baseUrl}/GetById${encodeURIComponent(String(id))}`;

        return this.httpClient.get<TaxRateDto>(url);
    }

    public Post(iddocumenttype: TaxRateRegisterRequest): Observable<TaxRateDto> {

        const url = `${this.baseUrl}/Register`;

        return this.httpClient.post<TaxRateDto>(url, iddocumenttype);
    }

    public Update(iddocumenttype: TaxRateDto): Observable<TaxRateDto> {

        const url = `${this.baseUrl}/Update`;

        return this.httpClient.put<TaxRateDto>(url, iddocumenttype);
    }

    public Delete(id: string): Observable<boolean> {

        const url = `${this.baseUrl}/Delete/${encodeURIComponent(String(id))}`;

        return this.httpClient.delete<boolean>(url);
    }

}
