import { CustomTranslateService } from "./customtranslate.service";
import { GlobalConstants } from "src/app/shared/const/global-constants";
import { Injectable } from "@angular/core";
import { MatDialog, MatDialogRef } from "@angular/material/dialog";
import { PopupButton } from "../models/popup-button";
import { PopupComponent } from "src/app/components";
import { PopupProperties } from "../models/popup-properties";

@Injectable({
    providedIn: "root"
})
export class PopupService {

    private translate: any = (key: string) =>
        this.translateService.instant('popup.' + key);

    public result: any;
    constructor(public dialog: MatDialog,
        private translateService: CustomTranslateService) { }

    public openCustomDialog(popupProperties: PopupProperties): MatDialogRef<any> {
        const dialogRef = this.dialog.open(PopupComponent, {
            width: popupProperties.width ? popupProperties.width : GlobalConstants.customWidthPopup,
            height: popupProperties.height ? popupProperties.height : GlobalConstants.customHeightPopup,
            data: { title: popupProperties.title, message: popupProperties.message, actions: popupProperties.actions }
        });

        return dialogRef;
    }

    public createConfirmPopup(title: string, msg: string): MatDialogRef<any> {
        const popConfirm = new PopupProperties(title, msg, "25rem", "auto");

        const buttonNo = new PopupButton(this.translate("button_no"), GlobalConstants.popupNoValue);
        const buttonYes = new PopupButton(this.translate("button_yes"), GlobalConstants.popupYesValue);
        popConfirm.actions = [];

        popConfirm.actions.push(buttonYes);
        popConfirm.actions.push(buttonNo);

        return this.openCustomDialog(popConfirm);
    }

    public openPopupAceptar(title: string, msg: string, width: string, height: string) {
        const popupSettings = new PopupProperties(title, msg, width, height);

        const buttonAceptar = new PopupButton(this.translate("button_ok"), GlobalConstants.popupConfirmValue);
        popupSettings.actions = [];

        popupSettings.actions.push(buttonAceptar);

        return this.openCustomDialog(popupSettings);
    }

    public createInformativePopup(msg: string): MatDialogRef<any> {
        const popInfo = new PopupProperties(this.translate("button_info"), msg, "31.25rem", "auto");

        const buttonOk = new PopupButton(this.translate("button_ok"), GlobalConstants.popupConfirmValue);
        popInfo.actions = [];

        popInfo.actions.push(buttonOk);

        return this.openCustomDialog(popInfo);
    }

}
