import { HttpClient } from '@angular/common/http';
import { IdDocumentTypeDto } from '../models/iddocumenttypedto';
import { IdDocumentTypeRegisterRequest } from '../models/iddocumenttyperegisterrequest';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';

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

    public Post(idDocumentTypeRegisterRequest: IdDocumentTypeRegisterRequest): Observable<IdDocumentTypeDto> {

        const url = `${this.baseUrl}/Register`;

        return this.httpClient.post<IdDocumentTypeDto>(url, idDocumentTypeRegisterRequest);
    }

    public Update(idDocumentTypeDto: IdDocumentTypeDto): Observable<IdDocumentTypeDto> {

        const url = `${this.baseUrl}/Update`;

        return this.httpClient.put<IdDocumentTypeDto>(url, idDocumentTypeDto);
    }

    public Delete(id: string): Observable<boolean> {

        const url = `${this.baseUrl}/Delete/${encodeURIComponent(String(id))}`;

        return this.httpClient.delete<boolean>(url);
    }

}
