import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class ErrorService {

    public GetErrorsFromHttp(httpErrorResponse: HttpErrorResponse): string[] {

        const errors: string[] = [];

        if (typeof (httpErrorResponse.error) === 'string') {
            errors.push(httpErrorResponse.error);
        }
        else {
            if (httpErrorResponse.error.ErrorMessage !== undefined) {
                if (Array.isArray(httpErrorResponse.error.ErrorMessage)) {
                    httpErrorResponse.error.ErrorMessage.forEach((error: string) => errors.push(error));
                }
                else {
                    errors.push(httpErrorResponse.error.ErrorMessage);
                }
            }
        }

        return errors;
    }

}
