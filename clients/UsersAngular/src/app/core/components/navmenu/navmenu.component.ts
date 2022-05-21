import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navmenu',
  templateUrl: './navmenu.component.html',
  styleUrls: ['./navmenu.component.css']
})
export class NavmenuComponent implements OnInit {

  title = 'UsersAngular';
  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  homeButtonClick(event: any) {
    console.log("Home button click.")
    this.router.navigate(['/home']);
  }

  usersButtonClick(event: any) {
    console.log("Users button click.")
    this.router.navigate(['/users']);
  }

  aboutButtonClick(event: any) {
    console.log("About button click.")
  }

  helpButtonClick(event: any) {
    console.log("Help button click.")
  }


}