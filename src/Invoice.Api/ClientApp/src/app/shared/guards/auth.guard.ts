import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { JWTService } from '../services';

export const authGuard = () => {

    const router = inject(Router);
    const jwtService = inject(JWTService);
    const exp = parseInt(jwtService.GetTokenExpiricyTime());

    return exp <= 0 ? router.navigate(['/login']) : true;
}    