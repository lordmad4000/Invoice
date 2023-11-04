import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Language } from '../models/language';
import { Observable } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';
import { CurrencyInfo } from '../models/currencyinfo';

@Injectable({
    providedIn: 'root',
})
export class CustomTranslateService {

    public supportedLanguages: Language[] = [
        { value: 'en-US', display: 'English' },
        { value: 'es-ES', display: 'Español' },
    ]

    private _currencyList: CurrencyInfo[] = new Array<CurrencyInfo>();
    public get currencyList(): CurrencyInfo[] {
        return this._currencyList;
    }

    private _currentLanguage: string;
    public get currentLanguage() {
        return this._currentLanguage;
    }

    public set currentLanguage(value: string) {
        this._currentLanguage = value;
        this.translateService.use(value);
    }

    constructor(private translateService: TranslateService, private httpClient: HttpClient) {
        this.loadCurrencyList();
        this._currentLanguage = this.supportedLanguages[0].value;
        this.translateService.addLangs(this.supportedLanguages.map(c => c.value));
        let language = this.translateService.getBrowserLang();
        language = this.supportedLanguages.map(c => c.value)
            .find(c => c.substring(1,2) === language?.substring(1,2));
        if (language !== undefined)
            this._currentLanguage = language;

        this.use();
    }

    private loadCurrencyList() {
        const url = '../../../assets/currency_list.json';
        this.httpClient.get(url).subscribe({
            next: (res: any) => {
                if (res) {
                    this._currencyList = res;
                }
            },
            error: (err: HttpErrorResponse) => {
                this._currencyList = new Array<CurrencyInfo>();
                this._currencyList.push(new CurrencyInfo());
                console.log(err.message)
            }
        })
    }

    public use(): void {
        this.translateService.use(this.currentLanguage);
    }

    public instant(key: string | string[], interpolateParams?: Object): any {
        return this.translateService.instant(key, interpolateParams)
    }

    public stream(key: string | string[], interpolateParams?: Object): Observable<any> {
        return this.translateService.stream(key, interpolateParams);
    }
}
