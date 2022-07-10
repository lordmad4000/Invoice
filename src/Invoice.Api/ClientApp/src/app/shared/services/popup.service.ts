import { Injectable, NgZone } from "@angular/core";
import { MatDialog, MatDialogRef } from "@angular/material/dialog";

import { PopupComponent } from "../components/popup/popup.component";
import { GlobalConstants } from "../const/global-constants";
import { PopupButton } from "../models/popup-button";
import { PopupProperties } from "../models/popup-properties";

@Injectable({
    providedIn: "root"
})
export class PopupService {

    public result: any;

    constructor(public dialog: MatDialog, private zone: NgZone) { }

    public openCustomDialog(popupProperties: PopupProperties): MatDialogRef<any> {
        const dialogRef = this.dialog.open(PopupComponent, {
            width: popupProperties.width ? popupProperties.width : GlobalConstants.customWidthPopup,
            height: popupProperties.height ? popupProperties.height : GlobalConstants.customHeightPopup,
            data: { title: popupProperties.title, message: popupProperties.message, actions: popupProperties.actions }
        });

        return dialogRef;
    }

    public createConfirmPopup(msg: string): MatDialogRef<any> {
        const popConfirm = new PopupProperties("REMOVE", msg, "400px", "auto");

        const buttonNo = new PopupButton("NO", GlobalConstants.popupNoValue);
        const buttonYes = new PopupButton("SI", GlobalConstants.popupYesValue);
        popConfirm.actions = [];

        popConfirm.actions.push(buttonNo);
        popConfirm.actions.push(buttonYes);

        return this.openCustomDialog(popConfirm);
    }

    public openPopupAceptar(titulo: string, msg: string, width: string, height: string) {
        const popupSettings = new PopupProperties(titulo, msg, width, height);

        const buttonAceptar = new PopupButton("ACEPTAR", GlobalConstants.popupConfirmValue);
        popupSettings.actions = [];

        popupSettings.actions.push(buttonAceptar);

        return this.openCustomDialog(popupSettings);
    }
    public createInformativePopup(msg: string): MatDialogRef<any> {
        const popInfo = new PopupProperties("POPUP.INFORMACION", msg, "500px", "auto");

        const buttonOk = new PopupButton("OK", GlobalConstants.popupConfirmValue);
        popInfo.actions = [];

        popInfo.actions.push(buttonOk);

        return this.openCustomDialog(popInfo);
    }

}
