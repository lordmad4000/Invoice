import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';

@Component({
  selector: 'app-taxrates',
  templateUrl: './taxrates.component.html',
  styleUrls: ['./taxrates.component.css'],
})
export class TaxRatesComponent {

  // constructor(private translateService: TranslateService) {
  //   this.translateService.addLangs(['en', 'es']);
  //   const language = this.translateService.getBrowserLang();
  //   if (language !== 'en' && language !== 'es') {
  //     this.translateService.setDefaultLang('en');
  //   }
  //   else {
  //     this.translateService.use(language);
  //   }
  // }

  constructor(private translateService: CustomTranslateService) {
  }

}