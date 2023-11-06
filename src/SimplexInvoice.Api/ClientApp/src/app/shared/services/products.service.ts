import { BasicProduct } from '../models/basicproduct';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { ProductDto } from '../models/productdto';
import { ProductRegisterRequest } from '../models/productregisterrequest';
import { environment } from 'src/environments/environment';

@Injectable()
export class ProductsService {

    private baseUrl = `${environment.API_BASE_URL}/api/Products`;

    constructor(protected httpClient: HttpClient) {
    }

    public GetAll(): Observable<Array<ProductDto>> {

        const url = `${this.baseUrl}/GetAll`;

        return this.httpClient.get<Array<ProductDto>>(url);
    }

    public Get(id: string): Observable<ProductDto> {

        const url = `${this.baseUrl}/GetById${encodeURIComponent(String(id))}`;

        return this.httpClient.get<ProductDto>(url);
    }

    public Post(productRegisterRequest: ProductRegisterRequest): Observable<ProductDto> {

        const url = `${this.baseUrl}/Register`;

        return this.httpClient.post<ProductDto>(url, productRegisterRequest);
    }

    public Update(productDto: ProductDto): Observable<ProductDto> {

        const url = `${this.baseUrl}/Update`;

        return this.httpClient.put<ProductDto>(url, productDto);
    }

    public Delete(id: string): Observable<boolean> {

        const url = `${this.baseUrl}/Delete/${encodeURIComponent(String(id))}`;

        return this.httpClient.delete<boolean>(url);
    }    

    public GetBasicProductsContainsName(name: string): Observable<BasicProduct[]> {

        const url = `${this.baseUrl}/GetBasicProductsContainsName${encodeURIComponent(String(name))}`;

        return this.httpClient.get<BasicProduct[]>(url);
    }

    public GetByCode(code: string): Observable<ProductDto> {

        const url = `${this.baseUrl}/GetByCode${encodeURIComponent(String(code))}`;

        return this.httpClient.get<ProductDto>(url);
    }

}
