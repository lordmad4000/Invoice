import { ActivatedRoute, Params, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { CustomTranslateService } from 'src/app/shared/services/customtranslate.service';
import { ErrorService } from 'src/app/shared';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { InvoiceDto } from 'src/app/shared/models/invoicedto';
import { InvoiceLineDto } from 'src/app/shared/models/invoicelinedto';
import { InvoicesService } from 'src/app/shared/services/invoices.service';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';
import { TotalTaxDto } from 'src/app/shared/models/totaltaxdto';

@Component({
  selector: 'app-invoices-view',
  templateUrl: './invoices-view.component.html',
  styleUrls: ['./invoices-view.component.css']
})
export class InvoicesViewComponent implements OnInit, OnDestroy {

  public formInvoice: FormGroup;
  public invoiceDto: InvoiceDto = new InvoiceDto;
  public invoiceLines = new Array<InvoiceLineDto>();
  public totalTaxes = new Array<TotalTaxDto>();
  public symbol: string = '';
  private subscription: Subscription | undefined;
  private id = "";
  public displayedColumns: string[] =
    ['packages',
      'productDescription',
      'quantity',
      'price',
      'taxBase',
      'tax',
      'discount',
      'total'
    ];

  constructor(
    private route: ActivatedRoute,
    private invoicesService: InvoicesService,
    private formBuilder: FormBuilder,
    private router: Router,
    private translateService: CustomTranslateService,
    private errorService: ErrorService,
    private snackBarService: SnackBarService) {

    this.formInvoice = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      number: [{ value: '', disabled: true }],
      description: [{ value: '', disabled: true }],
      date: [{ value: new Date, disabled: true }],
      companyName: [{ value: '', disabled: true }],
      companyIdDocumentType: [{ value: '', disabled: true }],
      companyDocumentNumber: [{ value: '', disabled: true }],
      companyStreet: [{ value: '', disabled: true }],
      companyCity: [{ value: '', disabled: true }],
      companyState: [{ value: '', disabled: true }],
      companyCountry: [{ value: '', disabled: true }],
      companyPostalCode: [{ value: '', disabled: true }],
      companyPhone: [{ value: '', disabled: true }],
      companyEmail: [{ value: '', disabled: true }],
      customerFullName: [{ value: '', disabled: true }],
      customerIdDocumentType: [{ value: '', disabled: true }],
      customerDocumentNumber: [{ value: '', disabled: true }],
      customerStreet: [{ value: '', disabled: true }],
      customerCity: [{ value: '', disabled: true }],
      customerState: [{ value: '', disabled: true }],
      customerCountry: [{ value: '', disabled: true }],
      customerPostalCode: [{ value: '', disabled: true }],
      customerPhone: [{ value: '', disabled: true }],
      customerEmail: [{ value: '', disabled: true }],
      totalTax: [{ value: 0, disabled: true }],
      totalDiscount: [{ value: 0, disabled: true }],
      totalTaxBase: [{ value: 0, disabled: true }],
      total: [{ value: 0, disabled: true }],
      currency: [{ value: '', disabled: true }],
      invoiceLines: [{ value: new Array<InvoiceLineDto>, disabled: true }],
      totalTaxes: [{ value: new Array<TotalTaxDto>, disabled: true }],
    });

  }

  ngOnInit(): void {
    this.subscription = this.route.params.subscribe((params: Params): void => {
      this.id = params['id'];
      this.getInvoice(this.id);
    });
  }

  public getCompanyAddress(): string {
    return this.invoiceDto.companyPostalCode + " " + this.invoiceDto.companyCity + " " + this.invoiceDto.companyState + " (" + this.invoiceDto.companyCountry + ")";
  }

  public getCompanyPhoneAndEmail(): string {
    return this.invoiceDto.companyPhone + " " + this.invoiceDto.companyEmail;
  }

  public getCustomerAddress(): string {
    return this.invoiceDto.customerPostalCode + " " + this.invoiceDto.customerCity + " " + this.invoiceDto.customerState + " (" + this.invoiceDto.customerCountry + ")";
  }

  public getCustomerPhoneAndEmail(): string {
    return this.invoiceDto.customerPhone + " " + this.invoiceDto.customerEmail;
  }

  private getInvoice(id: string) {
    this.invoicesService.Get(id)
      .subscribe({
        next: (res: InvoiceDto) => {
          if (res) {
            this.invoiceDto = res;
            this.invoiceLines = res.invoiceLines;
            this.totalTaxes = res.totalTaxes;
            this.setCurrencySymbol();
          }
        },
        error: (err: HttpErrorResponse) => {
          this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
        }
      })
  }

  private setCurrencySymbol() {
    const currencyInfo = this.translateService.currencyList.find(c => c.code === this.invoiceDto.currency);
    if (currencyInfo === undefined) {
      this.symbol = this.invoiceDto.currency;
    }
    else {
      this.symbol = currencyInfo.symbol;
    }
  }

  backButtonClick() {
    this.router.navigate(['/invoices/grid']);
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }

  }

}