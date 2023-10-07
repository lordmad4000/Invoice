import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
    providedIn: 'root',
})
export class CustomTranslateService {

    private _currentLanguage: string = 'en';

    public get currentLanguage() {
        return this._currentLanguage;
    }
    public set currentLanguage(value: string) {
        this._currentLanguage = value;
        this.translateService.use(value);
    }

    constructor(private translateService: TranslateService) {
        console.log("");
        this.translateService.addLangs(['en', 'es']);
        let language = this.translateService.getBrowserLang();
        if (language === undefined)
            language = 'en';

        this.currentLanguage = language;
    }

    public use() : void {
        this.translateService.use(this.currentLanguage);
    }

    public instant(key: string | string[], interpolateParams?: Object) : any {
        return this.translateService.instant(key, interpolateParams)
    }

    public stream(key: string | string[], interpolateParams?: Object) : Observable<any> {
        return this.translateService.stream(key, interpolateParams);
    }
}
