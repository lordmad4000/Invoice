import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PopupService } from '../../services/popup.service';

@Component({
  selector: 'app-navmenu',
  templateUrl: './navmenu.component.html',
  styleUrls: ['./navmenu.component.css']
})
export class NavmenuComponent implements OnInit {

  title = 'InvoiceApp';
  constructor(private router: Router, 
    private popupService: PopupService
    ) { }

  ngOnInit(): void {
    }

  loginButtonClick(event: any) {
    console.log("Login button click.")
    this.router.navigate(['/login']);
  }

  homeButtonClick(event: any) {
    console.log("Home button click.")
    this.router.navigate(['/home']);
  }

  usersButtonClick(event: any) {
    console.log("Users button click.")
    this.router.navigate(['/users/grid']);
  }

  aboutButtonClick(event: any) {
    console.log("About button click.")
    this.popupService.openPopupAceptar("About InvoiceApp", "Version: 0.1a", "400px", "");
  }

}
