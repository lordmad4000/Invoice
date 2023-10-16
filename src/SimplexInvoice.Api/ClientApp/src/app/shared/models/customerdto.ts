import { IdDocumentTypeDto } from "./iddocumenttypedto";

export class CustomerDto {
    id: string = '';
    firstName: string = '';
    lastName: string = '';
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