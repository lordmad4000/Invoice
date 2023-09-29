import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { IdDocumentTypeDto } from '../models/iddocumenttypedto';
import { IdDocumentTypeRegisterRequest } from '../models/iddocumenttyperegisterrequest';

@Injectable()
export class IdDocumentTypesService {

    private baseUrl = `${environment.API_BASE_URL}/api/IdDocumentTypes`;

    constructor(protected httpClient: HttpClient) {
    }

    public GetAll(): Observable<Array<IdDocumentTypeDto>> {

        const url = `${this.baseUrl}/GetAll`;

        return this.httpClient.get<Array<IdDocumentTypeDto>>(url);
    }

    public Get(id: string): Observable<IdDocumentTypeDto> {

        const url = `${this.baseUrl}/GetById${encodeURIComponent(String(id))}`;

        return this.httpClient.get<IdDocumentTypeDto>(url);
    }

    public Post(iddocumenttype: IdDocumentTypeRegisterRequest): Observable<IdDocumentTypeDto> {

        const url = `${this.baseUrl}/Register`;

        return this.httpClient.post<IdDocumentTypeDto>(url, iddocumenttype);
    }

    public Update(iddocumenttype: IdDocumentTypeDto): Observable<IdDocumentTypeDto> {

        const url = `${this.baseUrl}/Update`;

        return this.httpClient.put<IdDocumentTypeDto>(url, iddocumenttype);
    }

    public Delete(id: string): Observable<boolean> {

        const url = `${this.baseUrl}/Delete/${encodeURIComponent(String(id))}`;

        return this.httpClient.delete<boolean>(url);
    }

}
