import { HttpErrorResponse } from '@angular/common/http';
import { Location } from  '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { PopupService, UserService } from 'src/app/shared';
import { GlobalConstants } from 'src/app/shared/const/global-constants';

@Component({
  selector: 'app-users-view',
  templateUrl: './users-view.component.html',
  styleUrls: ['./users-view.component.css']
})
export class UsersViewComponent implements OnInit, OnDestroy {

  formUser: FormGroup;
  formLoginError: string = "";
  private subscription: Subscription | undefined;
  private id: string = "";

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private userService: UserService,
    private formBuilder: FormBuilder,
    private popupService: PopupService,
    private router: Router) {

    this.formUser = formBuilder.group({
      id: [{ value: '', disabled: true }],
      email: [{ value: '', disabled: true }],
      firstName: [{ value: '', disabled: true }],
      lastName: [{ value: '', disabled: true }],
    });
  }

  ngOnInit(): void {
    this.subscription = this.route.params.subscribe((params: Params): void => {
      this.id = params['id'];
      console.log(this.id);
      this.getUser(this.id);
    });
  }

  private getUser(id: string) {
    this.userService.Get(id)
      .subscribe({
        next: (res: any) => {
          const data = res;
          if (data) {
            this.formUser.patchValue(data);
          }
        },
        error: (err: HttpErrorResponse) => {
          console.log('Error al recuperar el usuario', err);
        }
      })
  }

  backButtonClick(event: any) {
    console.log("Back button.");
    this.location.back();
  }

  editButtonClick(event: any) {
    console.log("Edit button.");
    this.router.navigate(['/users/edit', `${this.id} `]);
  }

  deleteButtonClick(event: any) {
    console.log("Delete button.");
      this.popupService
      .createConfirmPopup("Do you want to remove the item?")
      .afterClosed()
      .subscribe(result => {
        if (result == GlobalConstants.popupYesValue) {
          this.removeItem();
        }
      });

  }

  removeItem() {
    this.userService.Delete(this.id)
      .subscribe({
        next: (res: any) => {          
          const data = res;
          if (data) {            
            this.popupService.openPopupAceptar("REMOVE", "Item removed.", "300px", "");
            this.router.navigate(['/users/grid']);
          }
          else{
            this.popupService.openPopupAceptar("REMOVE", "Item not removed.", "300px", "");
          }          
        },
        error: (err: HttpErrorResponse) => {
          console.log('Error al recuperar el usuario', err);
        }
      })
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }

  }


}