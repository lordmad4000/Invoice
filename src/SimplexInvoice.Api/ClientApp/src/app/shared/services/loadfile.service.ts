import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

@Injectable()
export class LoadFileService {

    constructor(protected httpClient: HttpClient) {
    }

    public LoadVersionFile(): Observable<any> {
        return this.httpClient.get('assets/version.json');
    }

}
