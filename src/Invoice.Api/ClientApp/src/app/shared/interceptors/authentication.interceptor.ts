import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { JWTService } from '../services';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {
    constructor(
        private jwtService: JWTService,
        private router: Router) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
        if (request.url.toLowerCase().includes('login') === false) {
            const auth_token = this.jwtService.GetTokenFromSessionStorage();
            httpHeaders = new HttpHeaders({
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + auth_token
            });
        }
        const httpRequest = request.clone({ headers: httpHeaders });

        return next.handle(httpRequest).pipe(
            catchError((error: HttpErrorResponse) => {
                if (error.status === 401) {
                    this.router.navigate(['/login']);
                }
                return throwError(error);
            }));
    }
}
