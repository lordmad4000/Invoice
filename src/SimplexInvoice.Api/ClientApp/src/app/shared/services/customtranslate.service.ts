import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Injectable()
export class CustomTranslateService {

    constructor(private translateService: TranslateService) {
        this.translateService.addLangs(['en', 'es']);
        const language = this.translateService.getBrowserLang();
        if (language !== 'en' && language !== 'es') {
            this.translateService.setDefaultLang('en');
        }
        else {
            this.translateService.use(language);
        }
    }
}
