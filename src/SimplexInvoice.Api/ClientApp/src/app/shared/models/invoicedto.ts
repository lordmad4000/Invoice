import { InvoiceLineDto } from "./invoicelinedto";
import { TotalTaxDto } from "./totaltaxdto";

export class InvoiceDto {
    id: string = '';
    number: string = '';
    description: string = '';
    date: string = '';
    companyName: string = '';
    companyIdDocumentType: string = '';
    companyDocumentNumber: string = '';
    companyStreet: string = '';
    companyCity: string = '';
    companyState: string = '';
    companyCountry: string = '';
    companyPostalCode: string = '';
    companyPhone: string = '';
    companyEmail: string = '';
    customerFullName: string = '';
    customerIdDocumentType: string = '';
    customerDocumentNumber: string = '';
    customerStreet: string = '';
    customerCity: string = '';
    customerState: string = '';
    customerCountry: string = '';
    customerPostalCode: string = '';
    customerPhone: string = '';
    customerEmail: string = '';
    totalTax: number = 0;
    totalDiscount: number = 0;
    totalTaxBase: number = 0;
    total: number = 0;
    currency: string = '';
    invoiceLines: Array<InvoiceLineDto> = new Array<InvoiceLineDto>;
    totalTaxes: Array<TotalTaxDto> = new Array<TotalTaxDto>;
}