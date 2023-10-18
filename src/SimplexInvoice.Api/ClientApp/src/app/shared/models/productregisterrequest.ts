export class ProductRegisterRequest {
    code: string = '';
    name: string = '';
    description: string = '';
    packageQuantity: number = 0.0;
    price: number = 0.0;
    currency: string = '';
    taxRateId: string = '';
}