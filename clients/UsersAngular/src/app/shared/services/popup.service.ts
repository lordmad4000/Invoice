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
        const popConfirm = new PopupProperties("POPUP.CONFIRMACION", msg, "325px", "auto");

        const buttonNo = new PopupButton("POPUP.NO", GlobalConstants.popupCancelValue);
        const buttonYes = new PopupButton("POPUP.SI", GlobalConstants.popupConfirmValue);
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
    //   public createErrorPopup(msg: string): MatDialogRef<any> {
    //     let str: string;
    //     if (msg) {
    //       if (msg.includes("[")) {
    //         str = msg.replace("[", "");
    //         str = str.replace("]", "");
    //         str = str.replace('"', "");
    //         str = str.replace('"', "");
    //       } else {
    //         str = msg;
    //       }
    //     }

    //     const popError = new PopupProperties("POPUP.ERROR", str, "500px", "auto");

    //     const buttonOk = new PopupButton("POPUP.OK", popupConfirmValue);
    //     popError.actions = [];

    //     popError.actions.push(buttonOk);

    //     return this.openCustomDialog(popError);
    //   }

    //   public openPopupWithGrid(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupGridComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         gridData: popupProperties.gridData,
    //         gridColumns: popupProperties.gridColumns
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupCitaForm(
    //     popupProperties: PopupProperties,
    //     cita: CitaAltaViewModel,
    //     citas: Array<CalendarEvent>,
    //     filtros?: CitaFiltroViewModel
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupCitaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       disableClose: false,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         form: popupProperties.data,
    //         alta: cita,
    //         citasList: citas,
    //         filtrosCarga: filtros
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupPagoDeudaCompleto(
    //     popupProperties: PopupProperties,
    //     idCliente: string,
    //     formasPago: MetodoPagoViewModel[],
    //     cajas: CajaViewModel[],
    //     isFromGrid?: boolean,
    //     importeTotalDeuda?: number,
    //     cantidadDinCuenta?: number,
    //     isVisitaLinea?: boolean,
    //     visitaLineaId?: string,
    //     isVisita?: boolean,
    //     visita?: VisitaViewModel
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupPagarDeudaVisitaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       disableClose: true,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: {
    //           idCliente,
    //           formasPago,
    //           cajas,
    //           isFromGrid,
    //           importeTotalDeuda,
    //           cantidadDinCuenta,
    //           isVisitaLinea,
    //           visitaLineaId,
    //           isVisita,
    //           visita
    //         }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   // tslint:disable-next-line: max-line-length
    //   public openPopupTraspasoAlmacen(
    //     popupProperties: PopupProperties,
    //     stock: StockViewModel,
    //     idAlmacen?: string
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupTraspasoAlmacenComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { stock, idAlmacen }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupAsignarAlmacen(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAsignarAlmacenComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupClientesAlertas(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupGridClienteAlertaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupMascotasAlertas(popupProperties: PopupProperties, animalId: string): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupGridMascotaAlertaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { animalId }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupFiltrarCitaEmpleado(
    //     popupProperties: PopupProperties,
    //     filtrosEmpleados: SearchItem[]
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupFiltrarCitaEmpleadoComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { filtrosEmpleados }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupFiltrarCitaAgenda(popupProperties: PopupProperties, filtrosAgendas: SearchItem[]): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupFiltrarCitaAgendaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { filtrosAgendas }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupFiltrarCitaSalas(popupProperties: PopupProperties, filtrosSalas: SearchItem[]): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupFiltrarCitaSalasComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { filtrosSalas }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupSeleccionarCentros(
    //     popupProperties: PopupProperties,
    //     centros: CentroViewModel[],
    //     centrosSeleccionados: CentrosSeleccionados
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupSeleccionarCentrosComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { centros, centrosSeleccionados }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupSeleccionarLicencias(
    //     popupProperties: PopupProperties,
    //     centros: CentroViewModel[],
    //     centrosIds: string[],
    //     licenciasActivas: LicenciasSeleccionadas
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupSeleccionarLicenciasComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { centros, centrosIds, licenciasActivas }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupSeleccionarAlmacenes(popupProperties: PopupProperties, selectItems: SearchItem[]): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupSeleccionarAlmacenesComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   // tslint:disable-next-line: max-line-length
    //   public openPopupSeleccionarProveedores(
    //     popupProperties: PopupProperties,
    //     selectItems: SearchItem[]
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupSeleccionarProveedoresComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupAgregarPackArticulos(popupProperties: PopupProperties, selectItems: SearchItem[]): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupPackArticulosComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupAgregarVisitaArticulos(
    //     popupProperties: PopupProperties,
    //     selectItems: SearchItem[]
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupVisitaArticulosComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupAgregarVisitaServicios(
    //     popupProperties: PopupProperties,
    //     selectItems: SearchItem[]
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupVisitaServiciosComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupAgregarVisitaPacks(popupProperties: PopupProperties, selectItems: SearchItem[]): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupVisitaPacksComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupAgregarPedidoArticulos(
    //     popupProperties: PopupProperties,
    //     selectItems: SearchItem[],
    //     proveedor: string
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupPedidoArticulosComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems, proveedor }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupAgregarPedidoFungibles(
    //     popupProperties: PopupProperties,
    //     selectItems: SearchItem[],
    //     proveedor: string
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupPedidoFungiblesComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems, proveedor }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupAgregarOtroProducto(popupProperties: PopupProperties, proveedor: string): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupComprasOtroProductoComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { proveedor }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupAgregarPackFungibles(popupProperties: PopupProperties, selectItems: SearchItem[]): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupPackFungiblesComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupAgregarPackServicios(popupProperties: PopupProperties, selectItems: SearchItem[]): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupPackServiciosComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupAgregarStock(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarStockComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpAgregarPropietario(popupProperties: PopupProperties, selectItems: SearchItem[]): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarPropietarioComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpAgregarClientePropietario(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarClientePropietarioComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpAgregarProveedor(popupProperties: PopupProperties, selectItems: SearchItem[]): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarProveedorComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpSelectCentroAlmacen(popupProperties: PopupProperties, selectItems: SearchItem[]): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarCentroAlmacenComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpAgregarEspecie(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarEspecieComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpAgregarRaza(popupProperties: PopupProperties, especieId?: string): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarRazaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { especieId }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpAgregarPelaje(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarPelajeComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpAgregarCapa(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarCapaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpAgregarFabricante(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarFabricanteComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpAgregarTipo(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarTipoComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpConvertirArticulo(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupConvertirArticuloComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpConvertirFungible(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupConvertirFungibleComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpConvertirServicio(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupConvertirServicioComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpAgregarDieta(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarDietaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpAgregarOrigen(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarOrigenComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpAgregarHabitat(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarHabitatComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpAgregarClasificacionMascota(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarClasificacionMascotaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupRegistrarFiltrosComunicaciones(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupRegistrofiltrocomunicacionesComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupOfertas(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupOfertasComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupCustomSelect(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupCustomSelectComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data,
    //         gridData: popupProperties.gridData
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupPerfilEconomico(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarPerfilEconomicoComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupCategoriaCliente(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarCategoriaClienteComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpPlazoPago(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarPlazoPagoComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       position: { top: "100px" },
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpPeriodoPago(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarPeriodoPagoComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpElegirPeriodoPago(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupElegirPeriodoPagoComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpElegirPlazoPago(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupElegirPlazoPagoComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpRecordatorioArticulo(popupProperties: PopupProperties, selectItems: SearchItem[]): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupRecordatorioArticuloComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpRecordatorioAnimal(
    //     popupProperties: PopupProperties,
    //     selectItems: SearchItem[],
    //     clientesAnimales: ClienteAnimalViewModel[],
    //     especieId: string,
    //     animal: AnimalViewModel
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupRecordatorioAnimalComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems },
    //         clientes: clientesAnimales,
    //         especie: especieId,
    //         mascota: animal
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpHorario(popupProperties: PopupProperties, selectItems: SearchItem[]): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupHorarioComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpElegirFormaPago(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupElegirFormaPagoComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpAgregarFormaPago(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarFormaPagoComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpPesoMascotas(popupProperties: PopupProperties, id: string): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupPesoMascotaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { id }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpGrupoHistorialMascotas(
    //     popupProperties: PopupProperties,
    //     historial: HistorialViewModel,
    //     animalId: string
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAnadirGrupoHistorialComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { historial, animalId }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpSeleccionarEspecialidadMascotas(
    //     popupProperties: PopupProperties,
    //     arraySeleccion: Array<EtiquetasViewModel>
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupSeleccionarEspecialidadMascotaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: arraySeleccion
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpMascotasClientes(popupProperties: PopupProperties, clienteId: string): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupMascotaClienteComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { clienteId }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpClientesMascotas(popupProperties: PopupProperties, animalId: string): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupClientesMascotaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { animalId }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpAgendaClientes(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgendaClientesComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpFormularioClientes(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupFormulariosClientesComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpComunicacionesClientes(
    //     popupProperties: PopupProperties,
    //     clienteId: string,
    //     email?: boolean,
    //     telefono?: boolean
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupComunicacionesClientesComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { clienteId, email, telefono }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpComunicacionesMascotas(
    //     popupProperties: PopupProperties,
    //     clienteId: string,
    //     email?: boolean,
    //     telefono?: boolean
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupComunicacionesMascotasComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { clienteId, email, telefono }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpTipoComunicacionesClientes(
    //     popupProperties: PopupProperties,
    //     email?: boolean,
    //     telefono?: boolean,
    //     tipo?: TipoComunicacion
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupTipoComunicacionesClientesComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { email, telefono, tipo }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpTipoComunicacionesMascotas(
    //     popupProperties: PopupProperties,
    //     email?: boolean,
    //     telefono?: boolean,
    //     tipo?: TipoComunicacion
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupTipoComunicacionesMascotasComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { email, telefono }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpComunicacionesMensajeClientes(
    //     popupProperties: PopupProperties,
    //     cliente: ClienteViewModel,
    //     tipo: TipoComunicacion
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupComunicacionClientesComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { cliente, tipo }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpPasarPedidoAAlbaran(
    //     popupProperties: PopupProperties,
    //     pedido: PedidoProveedorViewModel,
    //     update: boolean,
    //     numDocumento?: string,
    //     fechaDocumento?: Date,
    //     concepto?: string
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupPedidoAlbaranComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { pedido, update, numDocumento, fechaDocumento, concepto }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpPasarPedidoAFactura(
    //     popupProperties: PopupProperties,
    //     pedido: PedidoProveedorViewModel,
    //     update: boolean,
    //     numDocumento?: string,
    //     fechaDocumento?: Date,
    //     concepto?: string
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupPedidoFacturaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { pedido, update, numDocumento, fechaDocumento, concepto }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpPasarAlbaranAFactura(
    //     popupProperties: PopupProperties,
    //     albaran: AlbaranProveedorViewModel
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAlbaranFacturaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { albaran }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpPasarAlbaranesAFactura(
    //     popupProperties: PopupProperties,
    //     albaranes: Array<AlbaranProveedorViewModel>
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAlbaranesFacturaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { albaranes }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpPasarPedidosAAlbaran(popupProperties: PopupProperties, pedidos: Array<string>): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupPedidosAlbaranComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { pedidos }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpPasarPedidosAFactura(popupProperties: PopupProperties, pedidos: Array<string>): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupPedidosAFacturaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { pedidos }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpComunicacionesMensajeMascotas(
    //     popupProperties: PopupProperties,
    //     animalId: string,
    //     tipo: TipoComunicacion,
    //     plantilla?: boolean
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupComunicacionMascotasComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { animalId, plantilla, tipo }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpComprasAgregarStock(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupComprasAgregarStockComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpBuscadorRecetas(popupProperties: PopupProperties, selectItems: SearchItem[]): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupBuscadorRecetasComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpBuscadorServicios(popupProperties: PopupProperties, selectItems: SearchItem[]): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupBuscadorServiciosComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupNewClienteAlerta(popupProperties: PopupProperties, clienteId: string): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupNewClienteAlertaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { clienteId }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupAlertasClientes(popupProperties: PopupProperties, clienteId: string): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAlertasClientesComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { clienteId }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupViewClienteAlerta(
    //     popupProperties: PopupProperties,
    //     clienteAlerta: ClienteAlertaViewModel
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupViewClienteAlertaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { clienteAlerta }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupViewClienteAnimalAlerta(
    //     popupProperties: PopupProperties,
    //     clienteAnimalAlerta: ClienteAnimalAlertaViewModel
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupViewClienteAnimalAlertaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { clienteAnimalAlerta }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupBuscarPacksVentas(popupProperties: PopupProperties, selectItems: SearchItem[]): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupVentasBuscarPackComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpAgregarCliente(popupProperties: PopupProperties, clientes: Array<SearchItem>): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarClienteComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { clientes }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpEnvioEmailCompras(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupEnvioEmailComprasComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,

    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpEnvioEmailInformes(
    //     popupProperties: PopupProperties,
    //     email: string,
    //     clienteId: string,
    //     rows: InformeClienteViewModel[]
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupEnvioEmailInformesComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { email, clienteId, rows }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpSeleccionarVeterinario(
    //     popupProperties: PopupProperties,
    //     veterinarios: EmpleadoViewModel[],
    //     empleadoId?: string
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupSeleccionarVeterinarioComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { veterinarios, empleadoId }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupEnvioEmailHistoriales(
    //     popupProperties: PopupProperties,
    //     email: string,
    //     titulo: string
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupEnvioEmailHistorialesComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { email, titulo }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpEnvioEmailEstadosalud(
    //     popupProperties: PopupProperties,
    //     email: string,
    //     titulo: string
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupEnvioEmailEstadosaludComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { email, titulo }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpDeleteInformes(popupProperties: PopupProperties, rows: object[]): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupDeleteInformesComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { rows }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpDeleteEnviosPendientes(popupProperties: PopupProperties, rows: object[]): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupDeleteEnviosPendientesComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { rows }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpRecordatorioVacunasAnimal(
    //     popupProperties: PopupProperties,
    //     selectItems: SearchItem[]
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupRecordatorioVacunasAnimalComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { selectItems }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpAgregarPresupuestoPlantilla(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarPresupuestoPlantillaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpAnularVisita(popupProperties: PopupProperties, visita: VisitaViewModel): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAnularVisitaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { visita }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpEliminarVisita(popupProperties: PopupProperties, visita: VisitaViewModel): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupEliminarVisitaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { visita }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpRecepcionVisita(popupProperties: PopupProperties, visita: VisitaViewModel): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupRecepcionVisitaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { visita }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopUpCancelarVisita(popupProperties: PopupProperties, visita: VisitaViewModel): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupCancelarVisitaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { visita }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupVisitasClientes(popupProperties: PopupProperties, clienteId: string): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupVisitasClientesComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { clienteId }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupUltimaVisitaClientes(popupProperties: PopupProperties, clienteId: string): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupUltimaVisitaClientesComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { clienteId }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupUltimaVisitaMascotas(
    //     popupProperties: PopupProperties,
    //     clienteId: string,
    //     mascotaId: string
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupUltimaVisitaMascotasComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { mascotaId }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupVisitasMascotas(popupProperties: PopupProperties, mascotaId: string): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupVisitasMascotasComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { mascotaId }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupHistorialesMascota(popupProperties: PopupProperties, mascotaId: string): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupHistorialesMascotaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { mascotaId }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupNewMascotaAlerta(
    //     popupProperties: PopupProperties,
    //     clienteAnimal: ClienteAnimalViewModel
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupNewMascotaAlertaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { clienteAnimal }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupVerPackInfo(popupProperties: PopupProperties, packId: string): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupVerPackInfoComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { packId }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupVerCliente(popupProperties: PopupProperties, clienteId: ClienteViewModel): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupVerClienteComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { clienteId }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupVerMascota(popupProperties: PopupProperties, mascota: AnimalViewModel): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupVerMascotaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { mascota }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupVisitaCambioAlmacenStock(
    //     popupProperties: PopupProperties,
    //     articuloId: string,
    //     stockId?: string
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupVisitaLineaAlmacenStockComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { articuloId, stockId }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupVisitaCambioLicencia(
    //     popupProperties: PopupProperties,
    //     servicioId?: string,
    //     packId?: string,
    //     articuloId?: string,
    //     centroLicenciaId?: string,
    //     licenciasCentro?: Array<CentroLicenciaViewModel>
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupVisitaLineaLicenciaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { servicioId, packId, articuloId, centroLicenciaId, licenciasCentro }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupVisitaFactura(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupVisitaFacturaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupMostradorFactura(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupMostradorFacturaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupAgregarTrazabilidad(
    //     popupProperties: PopupProperties,
    //     linea: VisitaLineaViewModel
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarTrazabilidadComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { linea }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupVisitaCambioAlmacenStockFromPack(
    //     popupProperties: PopupProperties,
    //     lineasPack: VisitaLineaViewModel[]
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupVisitaLineaPackAlmacenStockComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { lineasPack }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupAgregarTrazabilidadPack(
    //     popupProperties: PopupProperties,
    //     lineasPack: VisitaLineaViewModel[]
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarTrazabilidadPackComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { lineasPack }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupDireccionFactura(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupDireccionFacturaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopUpDevolucionLineaVisitaConFactura(
    //     popupProperties: PopupProperties,
    //     visita: VisitaViewModel,
    //     lineas: VisitaLineaViewModel[],
    //     metodosPago: MetodoPagoViewModel[],
    //     cajas: CajaViewModel[],
    //     factura?: boolean,
    //     isVisita?: boolean,
    //     isVentaMostrador?: boolean
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupDevolucionLineaVisitaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { visita, lineas, metodosPago, cajas, factura, isVisita, isVentaMostrador }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupFacturaRecapitulativa(popupProperties: PopupProperties, isVisita: boolean): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupFacturaRecapitulativaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { isVisita }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupPedidoPendienteAlbaran(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupPedidoPendienteAlbaranComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupPedidoPendienteFactura(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupPedidoPendienteFacturaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupSeleccionarAlbaranEnPedido(
    //     popupProperties: PopupProperties,
    //     albaranes: WinbyAlbaranProveedorViewModel[]
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupSeleccionarAlbaranPedidoComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { albaranes }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupSeleccionarFacturaEnPedido(
    //     popupProperties: PopupProperties,
    //     facturas: FacturaProveedorViewModel[]
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupSeleccionarFacturaPedidoComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { facturas }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupFinalVenta(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupFinalVentaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupEnvioEmailPresupuestos(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupEnvioEmailPresupuestosComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupAgregarArticulo(popupProperties: PopupProperties): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarArticuloComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: popupProperties.data
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupLicenciasLineasPack(
    //     popupProperties: PopupProperties,
    //     lineasPack: VisitaLineaViewModel[]
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupVisitaLineaPackLicenciaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { lineasPack }
    //       }
    //     });

    //     return dialogRef;
    //   }

    //   public openPopupAgregarFormaPagoVisita(
    //     popupProperties: PopupProperties,
    //     pagosVisita: FormaPagoVisita[],
    //     metodosPago: MetodoPagoViewModel[]
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupAgregarPagoVisitaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { pagosVisita, metodosPago }
    //       }
    //     });

    //     return dialogRef;
    //   }
    //   public openPopupDineroCuentaVisita(
    //     popupProperties: PopupProperties,
    //     visita: VisitaViewModel,
    //     dineroSobrante: number
    //   ): MatDialogRef<any> {
    //     const dialogRef = this.dialog.open(PopupDineroCuentaVisitaComponent, {
    //       width: popupProperties.width ? popupProperties.width : customWidthPopup,
    //       height: popupProperties.height ? popupProperties.height : customHeightPopup,
    //       data: {
    //         title: popupProperties.title,
    //         message: popupProperties.message,
    //         actions: popupProperties.actions,
    //         data: { visita, dineroSobrante }
    //       }
    //     });

    //     return dialogRef;
    //   }
}
