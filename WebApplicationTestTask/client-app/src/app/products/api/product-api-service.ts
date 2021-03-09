import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { DataResponseModel } from 'src/app/shared/models/dataResponseModel';
import { ResponseModel } from 'src/app/shared/models/responseModel';
import { environment } from 'src/environments/environment';
import { ProductCreationModel } from '../models/product-creation-model';
import { ProductModel } from '../models/product-model';

@Injectable()
export class ProductsApiService {
    constructor(private httpClient: HttpClient) {}

    getProduct(id: number): Observable<DataResponseModel<ProductModel>>{
        return this.httpClient.get<DataResponseModel<ProductModel>>(`${environment.baseUrl}/api/products/${id}`);
    }

    getAllProducts(): Observable<DataResponseModel<ProductModel[]>> {
        return this.httpClient.get<DataResponseModel<ProductModel[]>>(`${environment.baseUrl}/api/products`);
    }

    createProduct(product: ProductCreationModel): Observable<DataResponseModel<ProductModel>> {
        return this.httpClient.post<DataResponseModel<ProductModel>>(`${environment.baseUrl}/api/products`, product);
    }

    updateProduct(id:number, product: ProductModel): Observable<ResponseModel> {
        return this.httpClient.put<ResponseModel>(`${environment.baseUrl}/api/products/${id}`, product);
    }

    deleteProduct(id: number): Observable<ResponseModel> {
        return this.httpClient.delete<ResponseModel>(`${environment.baseUrl}/api/products/${id}`);
    }
}