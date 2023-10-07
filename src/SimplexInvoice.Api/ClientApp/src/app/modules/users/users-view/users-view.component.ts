import { ActivatedRoute, Params, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { GlobalConstants } from 'src/app/shared/const/global-constants';
import { HttpErrorResponse } from '@angular/common/http';
import { Location } from  '@angular/common';
import { PopupService, UserService } from 'src/app/shared';
import { Subscription } from 'rxjs';
import { UserResponse } from 'src/app/shared/models/userresponse';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';

@Component({
  selector: 'app-users-view',
  templateUrl: './users-view.component.html',
  styleUrls: ['./users-view.component.css']
})
export class UsersViewComponent implements OnInit, OnDestroy {

  private translate: any = (key: string) =>
    this.translateService.instant('users.' + key);

  formUser: FormGroup;
  formLoginError = "";
  private subscription: Subscription | undefined;
  private id = "";

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private userService: UserService,
    private formBuilder: FormBuilder,
    private popupService: PopupService,
    private router: Router,
    private translateService: CustomTranslateService) {

    this.formUser = this.formBuilder.group({
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
        next: (res: UserResponse) => {
          if (res) {
            this.formUser.patchValue(res);
          }
        },
        error: (err: HttpErrorResponse) => {
          console.log('Error al recuperar el usuario', err);
        }
      })
  }

  backButtonClick() {
    this.location.back();
  }

  editButtonClick() {
    this.router.navigate(['/users/edit', `${this.id} `]);
  }

  deleteButtonClick() {
      this.popupService
      .createConfirmPopup(this.translate('popups.popup_delete_title'), this.translate('popups.popup_delete_message'))
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
        next: (res: boolean) => {          
          if (res) {            
            this.popupService.openPopupAceptar(this.translate('popups.popup_delete_title'), this.translate('popups.popup_deleted'), "18.75rem", "");
            this.router.navigate(['/users/grid']);
          }
          else{
            this.popupService.openPopupAceptar(this.translate('popups.popup_delete_title'), this.translate('popups.popup_not_deleted'), "18.75rem", "");
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