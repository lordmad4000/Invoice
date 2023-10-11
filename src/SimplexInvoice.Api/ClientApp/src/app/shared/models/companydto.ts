import { IdDocumentTypeDto } from "./iddocumenttypedto";

export class CompanyDto {
    id: string = '';
    name: string = '';
    idDocumentTypeId: string = '';
    idDocumentNumber: string = '';
    street: string = '';
    city: string = '';
    state: string = '';
    country: string = '';
    postalCode: string = '';
    phone: string = '';
    email: string = '';
    idDocumentType: IdDocumentTypeDto = new IdDocumentTypeDto;
}