import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'simplex-invoice-app';

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
