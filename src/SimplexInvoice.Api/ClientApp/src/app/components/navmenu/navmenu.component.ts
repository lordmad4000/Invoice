import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import {MatMenuModule} from '@angular/material/menu';
import { MatDividerModule } from '@angular/material/divider';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { JWTService, PopupService } from 'src/app/shared/services';

@Component({
  standalone: true,
  imports: [
    MatToolbarModule,
    MatIconModule,
    MatSidenavModule,
    MatDividerModule,
    MatListModule,
    AppRoutingModule,
    MatButtonModule,
    MatTooltipModule,
    MatMenuModule
  ], 
  selector: 'app-navmenu',
  templateUrl: './navmenu.component.html',
  styleUrls: ['./navmenu.component.css']
})

export class NavmenuComponent {

  title = 'SimplexInvoiceApp';
  constructor(private router: Router, 
    private popupService: PopupService,
    private jwtService: JWTService,
    ) { }

  toggleSidenavClick(sideNav: MatSidenav) {
    sideNav.toggle();
  }    

  loginButtonClick() {
    console.log("Login button click.")
    this.router.navigate(['/login']);
  }

  homeButtonClick() {
    console.log("Home button click.")
    this.router.navigate(['/home']);
  }

  idDocumentTypesButtonClick() {
    console.log("IdDocumentTypes button click.")
    this.router.navigate(['/iddocumenttypes/grid']);
  }

  taxRatesButtonClick() {
    this.router.navigate(['/taxrates/grid']);
  }

  usersButtonClick() {
    console.log("Users button click.")
    this.router.navigate(['/users/grid']);
  }

  aboutButtonClick() {
    console.log("About button click.")
    const tokenExpiricyTime = this.jwtService.GetTokenExpiricyTime();    
    this.popupService.openPopupAceptar("About SimplexInvoiceApp", "Version: 0.1a | Expiration token: " + tokenExpiricyTime + " sec", "400px", "");
  }

}
