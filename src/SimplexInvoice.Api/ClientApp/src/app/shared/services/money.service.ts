import { Injectable } from '@angular/core';

@Injectable()
export class MoneyService {

    private defaultDecimalsPlaces:number = 2;

    public round(num: number) {
        const factor = 10 ** this.defaultDecimalsPlaces;
        return Math.round(num * factor) / factor;  
    }

    public roundTo = (num: number, places: number) => {
      const factor = 10 ** places;
      return Math.round(num * factor) / factor;
    };

}