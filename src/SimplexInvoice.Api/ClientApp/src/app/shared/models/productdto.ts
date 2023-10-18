import { TaxRateDto } from "./taxratedto";

export class ProductDto {
    id: string = '';
    code: string = '';
    name: string = '';
    description: string = '';
    packageQuantity: number = 0.0;
    price: number = 0.0;
    currency: string = '';
    taxRateId: string = '';
    taxRate: TaxRateDto = new TaxRateDto;
}