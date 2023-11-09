import { CustomTranslateService } from './customtranslate.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class ErrorService {

    constructor(private translateService: CustomTranslateService) { }

    public HttpErrorResponseToStringArray(httpErrorResponse: HttpErrorResponse): string[] {
        const errors: string[] = [];

        if (typeof (httpErrorResponse.error) === 'string') {
            errors.push(httpErrorResponse.error);
        }
        else {
            if (httpErrorResponse.error !== undefined) {
                if (httpErrorResponse.error.errors !== undefined) {
                    if (httpErrorResponse.error.errors.length > 0) {
                        httpErrorResponse.error.errors.forEach((error: string) => errors.push(error));
                    }
                    else {
                        errors.push(httpErrorResponse.error.message);
                    }
                }
                else {
                    errors.push(httpErrorResponse.error);
                }
            }
        }
        if (errors.length === 0) {
            if (httpErrorResponse.message !== undefined) {
                errors.push(httpErrorResponse.message);
            }
            else {
                errors.push(this.translateService.instant('shared.errors.unknown'));
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
                if (httpErrorResponse.error.errors !== undefined &&
                    httpErrorResponse.error.errors.length > 0) {
                    errorMessages = httpErrorResponse.error.errors.join(" ");
                }
            }
        }
        if (errorMessages === undefined) {
            if (httpErrorResponse.message !== undefined) {
                errorMessages = httpErrorResponse.message;
            }
            else {
                errorMessages = this.translateService.instant('shared.errors.unknown');
            }
        }

        return errorMessages;
    }

}
