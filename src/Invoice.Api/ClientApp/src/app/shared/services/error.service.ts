import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class ErrorService {

    public GetErrorsFromHttp(httpErrorResponse: HttpErrorResponse): string[] {

        const errors: string[] = [];

        if (httpErrorResponse.error.errors === undefined) {
            errors.push(httpErrorResponse.error);
        }
        else {
            const errors: Array<string> = httpErrorResponse.error.errors[Object.keys(httpErrorResponse.error.errors)[0]];
            errors.forEach(clientError => {
                errors.push(clientError);
            });
        }

        return errors;
    }

}
