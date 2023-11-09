import { InvoiceLineRegisterRequest } from "./invoicelineregisterrequest";

export class InvoiceRegisterRequest {
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
    invoiceLines: Array<InvoiceLineRegisterRequest> = new Array<InvoiceLineRegisterRequest>;
}