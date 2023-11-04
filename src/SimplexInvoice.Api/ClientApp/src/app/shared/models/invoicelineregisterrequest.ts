export class InvoiceLineRegisterRequest {
    productCode: string = '';
    productName: string = '';
    productDescription: string = '';
    packages: number = 0;
    quantity: number = 0;
    price: number = 0;
    currency: string = '';
    taxName: string = '';
    taxRate: number = 0;
    discountRate: number = 0;
}