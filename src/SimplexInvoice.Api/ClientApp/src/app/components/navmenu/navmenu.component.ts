import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { MatMenuModule } from '@angular/material/menu';
import { MatDividerModule } from '@angular/material/divider';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { JWTService, PopupService } from 'src/app/shared/services';
import { MatSelectChange, MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-navmenu',
  templateUrl: './navmenu.component.html',
  styleUrls: ['./navmenu.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatSidenavModule,
    MatDividerModule,
    MatListModule,
    AppRoutingModule,
    MatButtonModule,
    MatTooltipModule,
    MatMenuModule,
    MatSelectModule,
    MatOptionModule,
    TranslateModule
  ]
})

export class NavmenuComponent {

  private translate: any = (key: string) =>
    this.translateService.instant('navmenu.' + key);

  indexInitialOption: string = 'en';
  languageOptions = [
    { value: 'en', display: 'English' },
    { value: 'es', display: 'Español' },
  ]

  constructor(
    private router: Router,
    private popupService: PopupService,
    private jwtService: JWTService,
    private translateService: CustomTranslateService
  ) {
    const currentLanguageOption = this.languageOptions.find(c => c.value === translateService.currentLanguage)
    if (currentLanguageOption !== undefined)
      this.indexInitialOption = currentLanguageOption.value;
  }

  toggleSidenavClick(sideNav: MatSidenav) {
    sideNav.toggle();
  }

  onChangeLanguage(event: MatSelectChange) {
    this.translateService.currentLanguage = event.value;
  }

  loginButtonClick() {
    this.router.navigate(['/login']);
  }

  homeButtonClick() {
    this.router.navigate(['/home']);
  }

  idDocumentTypesButtonClick() {
    this.router.navigate(['/iddocumenttypes/grid']);
  }

  taxRatesButtonClick() {
    this.router.navigate(['/taxrates/grid']);
  }

  companiesButtonClick() {
    this.router.navigate(['/companies/view']);
  }

  usersButtonClick() {
    this.router.navigate(['/users/grid']);
  }

  aboutButtonClick() {
    const tokenExpiricyTime = this.jwtService.GetTokenExpiricyTime();
    this.popupService.openPopupAceptar(this.translate('about_title'),
      this.translate('about_msg') + tokenExpiricyTime + 's',
      '28rem', '');
  }

}
