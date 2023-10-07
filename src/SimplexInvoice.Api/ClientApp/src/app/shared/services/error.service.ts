import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class ErrorService {

    public HttpErrorResponseToStringArray(httpErrorResponse: HttpErrorResponse): string[] {
        const errors: string[] = [];

        if (typeof (httpErrorResponse.error) === 'string') {
            errors.push(httpErrorResponse.error);
        }
        else {
            if (httpErrorResponse.error !== undefined) {
                if (httpErrorResponse.error.errors.length > 0) {
                    httpErrorResponse.error.errors.forEach((error: string) => errors.push(error));
                }
                else{
                    errors.push(httpErrorResponse.error.message);
                }
            }
        }

        return errors;
    }

    public HttpErrorResponseToString(httpErrorResponse: HttpErrorResponse): string {
        let errorMessages: string = '';
        if (typeof (httpErrorResponse.error) === 'string') {
            errorMessages = httpErrorResponse.error;
        }
        else {
            if (httpErrorResponse.error !== undefined) {
                errorMessages = httpErrorResponse.error.message;
                if (httpErrorResponse.error.errors.length > 0) {
                    errorMessages = httpErrorResponse.error.errors.join(" ");
                }
            }
        }

        return errorMessages;
    }

}
