import { BasicCustomer } from 'src/app/shared/models/basiccustomer';
import { CompaniesService, CustomTranslateService, CustomersService, ErrorService, TaxRatesService } from 'src/app/shared/services';
import { CompanyDto } from 'src/app/shared/models/companydto';
import { Component, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { CustomerDto } from 'src/app/shared/models/customerdto';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { InvoiceDto } from 'src/app/shared/models/invoicedto';
import { InvoiceLineRegisterRequest } from 'src/app/shared/models/invoicelineregisterrequest';
import { InvoiceRegisterRequest } from 'src/app/shared/models/invoiceregisterrequest';
import { InvoicesService } from 'src/app/shared/services/invoices.service';
import { MoneyService } from 'src/app/shared/services/money.service';
import { ProductDto } from 'src/app/shared/models/productdto';
import { Router } from '@angular/router';
import { SearchInput } from 'src/app/shared/models/searchinput';
import { SearchItem } from 'src/app/shared/models/searchitem';
import { SnackBarService } from 'src/app/shared/services/snackbar.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-invoices-new',
  templateUrl: './invoices-new.component.html',
  styleUrls: ['./invoices-new.component.css']
})

export class InvoicesNewComponent implements OnInit, OnDestroy {

  public selectedTaxRateId: string = '';
  public formInvoice: FormGroup;
  public formInvoiceLines: FormGroup;
  public invoiceLines: FormArray;
  public invoiceLinesVisible: boolean = false;
  public customerSearchInput: SearchInput[] = [];
  public customerSearchVisible: boolean = false;
  public basicCustomers: BasicCustomer[] = [];
  public customersList: SearchItem[] = [];
  public productSearchVisible: boolean = false;
  public productSearchInput: SearchInput[] = [];
  public productsList: SearchItem[] = [];
  private actualProductIndex: number = 0;
  private subscription: Subscription | undefined;

  constructor(
    private invoicesService: InvoicesService,
    private companiesService: CompaniesService,
    private customersService: CustomersService,
    private formBuilder: FormBuilder,
    private errorService: ErrorService,
    private renderer: Renderer2,
    private router: Router,
    private snackBarService: SnackBarService,
    private translateService: CustomTranslateService,
    private moneyService: MoneyService) {

    this.formInvoiceLines = this.formBuilder.group({
      invoiceLines: this.formBuilder.array([this.createInvoiceLine()])
    });
    this.invoiceLines = this.formInvoiceLines.get('invoiceLines') as FormArray;
    this.formInvoice = this.createInvoice();
  }

  ngOnInit(): void {
    this.addInvoiceLine();
    this.loadAndSetCompanyData();
  }

  loadCustomerSearchInputData() {
    this.customerSearchInput = [
      {
        id: '1',
        inputText: this.translateService.instant('customers.search.byname'),
        chipText: this.translateService.instant('customers.search.byname_chip')
      },
      {
        id: '2',
        inputText: this.translateService.instant('customers.search.byiddocumentnumber'),
        chipText: this.translateService.instant('customers.search.byiddocumentnumber_chip')
      },
      {
        id: '3',
        inputText: this.translateService.instant('customers.search.byemail'),
        chipText: this.translateService.instant('customers.search.byemail_chip')
      }
    ]
  }

  loadProductSearchInputData() {
    this.productSearchInput = [
      {
        id: '1',
        inputText: this.translateService.instant('products.search.byname'),
        chipText: this.translateService.instant('products.search.byname_chip')
      }
    ]
  }

  private createInvoice(): FormGroup {
    return this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      number: [{ value: '', disabled: true }],
      description: [{ value: '', disabled: true }],
      date: [{ value: '', disabled: true }],
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
      invoiceLines: [{ value: '', disabled: true }],
      totalTaxes: [{ value: '', disabled: true }],
    })
  }

  private createInvoiceLine(): FormGroup {
    return this.formBuilder.group({
      productCode: [{ value: '', disabled: false }],
      productName: [{ value: '', disabled: true }],
      productDescription: [{ value: '', disabled: true }],
      packages: [{ value: 0, disabled: true }],
      packageQuantity: [{ value: 0, disabled: true }],
      quantity: [{ value: 0, disabled: true }],
      price: [{ value: 0, disabled: true }],
      currency: [{ value: '', disabled: true }],
      taxName: [{ value: '', disabled: true }],
      taxRate: [{ value: 0, disabled: true }],
      discountRate: [{ value: 0, disabled: true }],
      tax: [{ value: 0, disabled: true }],
      discount: [{ value: 0, disabled: true }],
      taxBase: [{ value: 0, disabled: true }],
      total: [{ value: 0, disabled: true }],
      ok: [{ value: false, disabled: true }],
    });
  }

  getInvoiceLinesControls(): FormArray<any> {
    return this.formInvoiceLines.get('invoiceLines') as FormArray;
  }

  addInvoiceLine() {
    this.invoiceLines = this.formInvoiceLines.get('invoiceLines') as FormArray;
    this.invoiceLines.push(this.createInvoiceLine());
  }

  removeInvoiceLine(index: any) {
    console.log(index);
    (<FormArray>this.formInvoiceLines.get("invoiceLines"))
      .removeAt(index);
    const invoiceLinesLength = (<FormArray>this.formInvoiceLines.get("invoiceLines")).length;
    if (invoiceLinesLength == 1 || index >= invoiceLinesLength) {
      this.addInvoiceLine();
    }
    this.calculateTotals();
  }

  private getCustomersContainsFullName(fullName: string) {
    this.customersService.GetBasicCustomersContainsFullName(fullName).subscribe({
      next: (res: BasicCustomer[]) => {
        if (res) {
          this.basicCustomers = res;
        }
      },
      error: (err: HttpErrorResponse) => {
        this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
      }
    })

  }

  private getCustomerById(id: string){
    this.customersService.Get(id).subscribe({
      next: (res: CustomerDto) => {
        if (res) {
          this.setCustomerData(res);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
      }
    })
  }  

  saveButtonClick() {
    const invoice = this.setInvoiceDataFromFormData();
    if (invoice.invoiceLines.length === 0) {
      this.snackBarService.openSnackBar('Must be at least 1 valid line.', 2, '#red-snackbar');
      return;
    }
    this.invoicesService.Post(invoice).subscribe({
      next: (res: InvoiceDto) => {
        if (res) {
          this.backButtonClick();
        }
      },
      error: (err: HttpErrorResponse) => {
        this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
      }
    });
  }

  setInvoiceDataFromFormData(): InvoiceRegisterRequest {
    const invoice: InvoiceRegisterRequest = {
      number: crypto.randomUUID().toString().substring(0, 20),
      description: 'Invoice',
      date: this.formInvoice.get('date')?.value,
      companyName: this.formInvoice.get('companyName')?.value,
      companyIdDocumentType: this.formInvoice.get('companyIdDocumentType')?.value,
      companyDocumentNumber: this.formInvoice.get('companyDocumentNumber')?.value,
      companyStreet: this.formInvoice.get('companyStreet')?.value,
      companyCity: this.formInvoice.get('companyCity')?.value,
      companyState: this.formInvoice.get('companyState')?.value,
      companyCountry: this.formInvoice.get('companyCountry')?.value,
      companyPostalCode: this.formInvoice.get('companyPostalCode')?.value,
      companyPhone: this.formInvoice.get('companyPhone')?.value,
      companyEmail: this.formInvoice.get('companyEmail')?.value,
      customerFullName: this.formInvoice.get('customerFullName')?.value,
      customerIdDocumentType: this.formInvoice.get('customerIdDocumentType')?.value,
      customerDocumentNumber: this.formInvoice.get('customerDocumentNumber')?.value,
      customerStreet: this.formInvoice.get('customerStreet')?.value,
      customerCity: this.formInvoice.get('customerCity')?.value,
      customerState: this.formInvoice.get('customerState')?.value,
      customerCountry: this.formInvoice.get('customerCountry')?.value,
      customerPostalCode: this.formInvoice.get('customerPostalCode')?.value,
      customerPhone: this.formInvoice.get('customerPhone')?.value,
      customerEmail: this.formInvoice.get('customerEmail')?.value,
      invoiceLines: [],
    }
    for (let i = 0; i < this.invoiceLines.length; i++) {
      if (this.isValidInvoiceLine(i, false)) {
        const invoceLine: InvoiceLineRegisterRequest = {
          productCode: this.invoiceLinesGetValue(i, 'productCode'),
          productName: this.invoiceLinesGetValue(i, 'productName'),
          productDescription: this.invoiceLinesGetValue(i, 'productDescription'),
          packages: this.invoiceLinesGetValue(i, 'packages'),
          quantity: this.invoiceLinesGetValue(i, 'quantity'),
          price: this.invoiceLinesGetValue(i, 'price'),
          currency: this.invoiceLinesGetValue(i, 'currency'),
          taxName: this.invoiceLinesGetValue(i, 'taxName'),
          taxRate: this.invoiceLinesGetValue(i, 'taxRate'),
          discountRate: this.invoiceLinesGetValue(i, 'discountRate'),
        }
        invoice.invoiceLines.push(invoceLine);
      }
    }

    return invoice;
  }

  onChangesCustomerSearch(input: SearchInput) {
    if (input.inputText.length < 3) {
      this.customersList = [];
    }
    else {
      this.getCustomersContainsFullName(input.inputText);
      this.customersList = this.basicCustomers.map(c => ({
        id: c.id,
        description: c.fullName + " - " + c.idDocumentNumber + " - " + c.phone + " - " + c.email,
        searchId: input.id,
      }));
    }
  }

  onChangesProductsSearch(input: SearchInput) {
    if (input.inputText.length < 3) {
      this.productsList = [];
    }
    else {
      let products = this.getProducts();
      products = products.filter(c => c.name.toLowerCase().includes(input.inputText.toLowerCase()));
      this.productsList = products.map(c => ({
        id: c.id,
        description: c.name + " - " + c.price + " - " + c.taxRate.name,
        searchId: input.id
      }));
    }
  }

  onSelectedCustomer(item: SearchItem) {
    this.getCustomerById(item.id);
  }

  setCustomerData(customer: CustomerDto) {
    if (customer !== undefined) {
      this.customerSearchVisible = false;
      const customerFullName = customer.firstName + ' ' + customer.lastName;
      this.formInvoice.get('customerFullName')?.setValue(customerFullName);
      this.formInvoice.get('customerIdDocumentType')?.setValue(customer.idDocumentType.name);
      this.formInvoice.get('customerDocumentNumber')?.setValue(customer.idDocumentNumber);
      this.formInvoice.get('customerStreet')?.setValue(customer.street);
      this.formInvoice.get('customerCity')?.setValue(customer.city);
      this.formInvoice.get('customerState')?.setValue(customer.state);
      this.formInvoice.get('customerCountry')?.setValue(customer.country);
      this.formInvoice.get('customerPostalCode')?.setValue(customer.postalCode);
      this.formInvoice.get('customerPhone')?.setValue(customer.phone);
      this.formInvoice.get('customerEmail')?.setValue(customer.email);
      this.formInvoice.get('date')?.enable();
      this.renderer.selectRootElement('#invoice-date-field').focus();
      this.invoiceLinesVisible = true;
    }
  }

  loadAndSetCompanyData() {
    this.companiesService.Get()
      .subscribe({
        next: (res: CompanyDto) => {
          if (res) {
            this.setCompanyData(res);
          }
        },
        error: (err: HttpErrorResponse) => {
          this.snackBarService.openSnackBar(this.errorService.HttpErrorResponseToString(err));
        }
      })

  }

  setCompanyData(company: CompanyDto) {
    this.formInvoice.get('companyName')?.setValue(company.name);
    this.formInvoice.get('companyIdDocumentType')?.setValue(company.idDocumentType.name);
    this.formInvoice.get('companyDocumentNumber')?.setValue(company.idDocumentNumber);
    this.formInvoice.get('companyStreet')?.setValue(company.street);
    this.formInvoice.get('companyCity')?.setValue(company.city);
    this.formInvoice.get('companyState')?.setValue(company.state);
    this.formInvoice.get('companyCountry')?.setValue(company.country);
    this.formInvoice.get('companyPostalCode')?.setValue(company.postalCode);
    this.formInvoice.get('companyPhone')?.setValue(company.phone);
    this.formInvoice.get('companyEmail')?.setValue(company.email);
  }

  onSelectedProduct(item: SearchItem) {
    const product = this.getProducts().find(c => c.id === item.id);
    if (product !== undefined) {
      this.productSearchVisible = false;
      this.invoiceLinesSetValue(this.actualProductIndex, 'productCode', product.code);
      this.onProductFocusOut(this.actualProductIndex);
    }
  }

  showCustomerSearch() {
    this.loadCustomerSearchInputData();
    this.customersList = [];
    this.customerSearchVisible = true;
  }

  showProductsSearch() {
    this.loadProductSearchInputData();
    this.productsList = [];
    this.productSearchVisible = true;
  }

  backButtonClick() {
    this.router.navigate(['/invoices/grid']);
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  onProductCodeKeyupF2(index: number) {
    this.actualProductIndex = index;
    this.showProductsSearch();
  }

  onKeyupEnterDate(event: any) {
    console.log(event)
  }

  onFieldKeyupEnter(nextElement: string, index: number) {
    if (nextElement.includes('packages')) {
      this.onProductFocusOut(index);
    }
    if (nextElement.includes('productcode')){
      this.setInvoiceLineOk(index - 1);
    }
    this.renderer.selectRootElement(nextElement).focus();
  }

  onProductFocus(index: number) {
    if (index === 0 || this.isValidInvoiceLine(index - 1)) {     
      const nextElement = 'productcode' + (index + 1);
      let element = document.getElementById(nextElement);
      if (element === null) {
        this.addInvoiceLine();
      }
    }
  }

  onProductFocusOut(index: number) {
    if (index === 0 || this.isValidInvoiceLine(index - 1)) {    
      const productCode = this.invoiceLines.at(index).get('productCode')?.value;
      const product = this.getProductInfo(productCode);
      if (product !== undefined) {
        this.setInvoiceLinesValues(index, product);
        this.enableInputs(index);
        this.calculateTotalsFromPrice(index);
        this.renderer.selectRootElement('#packages' + index).focus();
      }
      else {
        this.clearInvoiceLineData(index);
      }
    }
  }

  onChangesPackages(index: number) {
    const packageQuantity = this.invoiceLinesGetValue(index, 'packageQuantity');
    if (packageQuantity > 0) {
      const packages = this.invoiceLinesGetValue(index, 'packages');
      this.invoiceLinesSetValue(index, 'quantity', packageQuantity * packages);
      this.calculateTotalsFromPrice(index);
    }
  }

  onChangesPrice(index: number) {
    this.calculateTotalsFromPrice(index);
  }

  onChangesQuantity(index: number) {
    this.calculateTotalsFromPrice(index);
  }

  onChangesDiscount(index: number) {
    this.calculateTotalsFromPrice(index);
  }

  onChangesTotal(index: number) {
    this.calculateTotalsFromTotal(index);
  }

  calculateTotalsFromTotal(index: number) {
    if (this.isValidInvoiceLine(index, false) === false) {
      return;
    }
    let quantity: number = this.invoiceLinesGetValue(index, 'quantity');
    const taxRate: number = this.invoiceLinesGetValue(index, 'taxRate');
    let total: number = this.invoiceLinesGetValue(index, 'total');
    total = this.moneyService.round(total);
    let tax = total - total / (1 + taxRate / 100);
    tax = this.moneyService.round(tax);
    let taxBase = total - tax;
    taxBase = this.moneyService.round(taxBase);
    if (quantity === 0) {
      quantity = 1;
    }
    quantity = this.moneyService.roundTo(quantity, 3);
    let price = taxBase / quantity;
    price = this.moneyService.round(price);
    this.invoiceLinesSetValue(index, 'price', price);
    this.invoiceLinesSetValue(index, 'taxBase', taxBase);
    this.invoiceLinesSetValue(index, 'tax', tax);
    this.invoiceLinesSetValue(index, 'discountRate', 0);
    this.invoiceLinesSetValue(index, 'discount', 0);
    this.invoiceLinesSetValue(index, 'total', total);
    this.calculateTotals();
  }

  calculateDisclunt() {
    let total = 100;
    let discountRate = 10;
    total = this.moneyService.round(total);
    let discount = total - total / (1 + discountRate / 100);
    discount = this.moneyService.round(discount);
    let taxBase = total + discount;
    taxBase = this.moneyService.round(taxBase);

  }

  calculateTotalsFromPrice(index: number) {
    if (this.isValidInvoiceLine(index, false) === false) {
      return;
    }
    let quantity: number = this.invoiceLinesGetValue(index, 'quantity');
    let price: number = this.invoiceLinesGetValue(index, 'price');
    const taxRate: number = this.invoiceLinesGetValue(index, 'taxRate');
    let discountRate: number = this.invoiceLinesGetValue(index, 'discountRate');
    discountRate = this.moneyService.roundTo(discountRate, 0);
    let total: number = this.invoiceLinesGetValue(index, 'total');
    let taxBase: number = 0;
    let tax: number = 0;
    let discount: number = 0;
    price = this.moneyService.round(price);
    quantity = this.moneyService.roundTo(quantity, 3);
    taxBase = quantity * price;
    taxBase = this.moneyService.round(taxBase);
    tax = (taxBase * taxRate) / 100;
    tax = this.moneyService.round(tax);
    discount = (taxBase * discountRate) / 100;
    discount = this.moneyService.round(discount);
    total = taxBase + tax - discount;
    total = this.moneyService.round(total);
    this.invoiceLinesSetValue(index, 'price', price);
    this.invoiceLinesSetValue(index, 'taxBase', taxBase);
    this.invoiceLinesSetValue(index, 'tax', tax);
    this.invoiceLinesSetValue(index, 'discountRate', discountRate);
    this.invoiceLinesSetValue(index, 'discount', discount);
    this.invoiceLinesSetValue(index, 'total', total);
    this.calculateTotals();
  }

  calculateTotals() {
    let totalTaxBase = 0;
    let totalTax = 0;
    let totalDiscount = 0;
    let total = 0;
    for (let i = 0; i < this.invoiceLines.length; i++) {
      totalTaxBase += this.invoiceLinesGetValue(i, "taxBase");
      totalTax += this.invoiceLinesGetValue(i, "tax");
      totalDiscount += this.invoiceLinesGetValue(i, "discount");
      total += this.invoiceLinesGetValue(i, "total");
    }
    totalTaxBase = this.moneyService.round(totalTaxBase);
    totalTax = this.moneyService.round(totalTax);
    totalDiscount = this.moneyService.round(totalDiscount);
    total = this.moneyService.round(total);
    this.formInvoice.get('totalTaxBase')?.setValue(totalTaxBase);
    this.formInvoice.get('totalTax')?.setValue(totalTax);
    this.formInvoice.get('totalDiscount')?.setValue(totalDiscount);
    this.formInvoice.get('total')?.setValue(total);
  }

  setInvoiceLinesValues(index: number, product: ProductDto) {
    this.invoiceLinesSetValue(index, 'productName', product.name);
    this.invoiceLinesSetValue(index, 'productDescription', product.description);
    this.invoiceLinesSetValue(index, 'packageQuantity', product.packageQuantity);
    this.invoiceLinesSetValue(index, 'quantity', 1);
    this.invoiceLinesSetValue(index, 'price', product.price);
    this.invoiceLinesSetValue(index, 'taxName', product.taxRate.name);
    this.invoiceLinesSetValue(index, 'taxRate', product.taxRate.value);
    this.invoiceLinesSetValue(index, 'currency', product.currency);
  }

  setInvoiceLineOk(index: number) {
    if (this.isValidInvoiceLine(index, false)) {
      this.invoiceLinesSetValue(index, 'ok', true);
    }
    this.invoiceLinesSetValue(index, 'false', true);
  }

  enableInputs(index: number) {
    this.invoiceLines.at(index).get('packages')?.enable();
    this.invoiceLines.at(index).get('quantity')?.enable();
    this.invoiceLines.at(index).get('price')?.enable();
    this.invoiceLines.at(index).get('discountRate')?.enable();
    this.invoiceLines.at(index).get('total')?.enable();
  }

  addNewLine(index: number) {
    if (this.isValidInvoiceLine(index)) {
      const nextElement = 'productcode' + (index + 1);
      let element = document.getElementById(nextElement);
      if (element === null) {
        this.addInvoiceLine();
      }
    }
  }

  clearInvoiceLineData(index: number) {
    this.invoiceLines.at(index).get('productCode')?.setValue('');
    this.invoiceLines.at(index).get('productName')?.setValue('');
    this.invoiceLines.at(index).get('productDescription')?.setValue('');
    this.invoiceLines.at(index).get('packages')?.setValue(0);
    this.invoiceLines.at(index).get('quantity')?.setValue(0);
    this.invoiceLines.at(index).get('price')?.setValue(0);
    this.invoiceLines.at(index).get('taxBase')?.setValue(0);
    this.invoiceLines.at(index).get('currency')?.setValue('');
    this.invoiceLines.at(index).get('taxName')?.setValue('');
    this.invoiceLines.at(index).get('taxRate')?.setValue(0);
    this.invoiceLines.at(index).get('discountRate')?.setValue(0);
    this.invoiceLines.at(index).get('tax')?.setValue(0);
    this.invoiceLines.at(index).get('discount')?.setValue(0);
    this.invoiceLines.at(index).get('total')?.setValue(0);
  }

  isValidInvoiceLine(index: number, showErrorMessage: boolean = true): boolean {
    let isValid: boolean = true;
    if (this.isEmptyOrSpaces(this.invoiceLinesGetValue(index, 'productCode'))) {
      isValid = false;
    }
    if (!this.isNumeric(this.invoiceLinesGetValue(index, 'packages'))) {
      isValid = false;
    }
    if (!this.isNumeric(this.invoiceLinesGetValue(index, 'quantity'))) {
      isValid = false;
    }
    if (!this.isNumeric(this.invoiceLinesGetValue(index, 'price'))) {
      isValid = false;
    }
    if (!this.isNumeric(this.invoiceLinesGetValue(index, 'discount'))) {
      isValid = false;
    }
    if (!this.isNumeric(this.invoiceLinesGetValue(index, 'total'))) {
      isValid = false;
    }

    if (!isValid && showErrorMessage) {
      this.snackBarService.openSnackBar("The last edited line is not correct.", 2, '', 'red-snackbar');
    }

    return isValid;
  }

  setFocusOnNextProductInput(index: number) {
    for (; index < 100; index++) {
      const nextElement = '#productcode' + (index + 1);
      document.getElementById(nextElement);
      try {
        this.renderer?.selectRootElement(nextElement);
      }
      catch {
        this.addInvoiceLine();
      }
    }
  }

  private invoiceLinesSetValue(index: number, controlName: string, value: any) {
    this.invoiceLines.at(index).get(controlName)?.setValue(value);
  }

  invoiceLinesGetValue(index: number, controlName: string): any {
    return this.invoiceLines.at(index).get(controlName)?.value;
  }

  isEmptyOrSpaces(str: string): boolean {
    const result = str === null || str.match(/^ *$/) !== null;

    return result;
  }

  isNumeric(str: string): boolean {
    const num: number = +str;
    return !isNaN(num) && !isNaN(parseFloat(str))
  }

  getProductInfo(code: string): ProductDto | undefined {
    const data = this.getProducts();
    return data.find(c => c.code.toLowerCase() === code.toLowerCase());
  }

  getProducts(): ProductDto[] {
    return [
      {
        id: '1',
        code: 'ALC',
        name: 'Alcachofa',
        description: 'Alcachofa de tudela',
        packageQuantity: 1,
        price: 1.5,
        currency: 'EUR',
        taxRateId: '',
        taxRate: {
          id: '',
          name: '10%',
          value: 10
        }
      },
      {
        id: '2',
        code: 'BJ',
        name: 'Berenjena',
        description: 'Berenjena',
        packageQuantity: 1,
        price: 1,
        currency: 'EUR',
        taxRateId: '',
        taxRate: {
          id: '',
          name: '4%',
          value: 4
        }
      },
      {
        id: '3',
        code: 'MF',
        name: 'Manzana Fuji',
        description: 'Manzana Fuji',
        packageQuantity: 1,
        price: 2.25,
        currency: 'EUR',
        taxRateId: '',
        taxRate: {
          id: '',
          name: '4%',
          value: 4
        }
      },
      {
        id: '4',
        code: 'SAN',
        name: 'Sandia',
        description: 'Sandia',
        packageQuantity: 1,
        price: 0.75,
        currency: 'EUR',
        taxRateId: '',
        taxRate: {
          id: '',
          name: '10%',
          value: 10
        }
      },
    ]
  }

  onChange(i: any) {
    console.log(i);
  }

}
