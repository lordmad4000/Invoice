import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { User } from 'src/app/shared/models/user';
import { PopupService } from 'src/app/shared/services/popup.service';
import { GlobalConstants } from 'src/app/shared/const/global-constants';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

  users: User[] = [];

  constructor(
    private httpClient: HttpClient,
    private popupService: PopupService,
    private router: Router) {
  }

  ngOnInit(): void {    
    // this.popupService
    //   .createConfirmPopup("Do you want to remove the item?")
    //   .afterClosed()
    //   .subscribe(result => {
    //     if (result == GlobalConstants.popupYesValue) {
    //       this.popupService.openPopupAceptar("REMOVE", "Item removed.", "300px", "");
    //     }
    //     else {
    //       this.popupService.openPopupAceptar("REMOVE", "Item not removed.", "300px", "");
    //     }
    //   });
  }
}
