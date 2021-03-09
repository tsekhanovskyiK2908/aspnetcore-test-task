import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { DataResponseModel } from "src/app/shared/models/dataResponseModel";
import { environment } from "src/environments/environment";
import { CustomerCreationModel } from "../models/customer-creation-model";
import { CustomerModel } from "../models/customer-model";

@Injectable()
export class CustomersApiService {
    constructor(private httpClient: HttpClient) {}

    getCustomer(id: number): Observable<DataResponseModel<CustomerModel>>{
        return this.httpClient.get<DataResponseModel<CustomerModel>>(`${environment.baseUrl}/api/customers/${id}`);
    }

    getAllCustomers(): Observable<DataResponseModel<CustomerModel[]>> {
        return this.httpClient.get<DataResponseModel<CustomerModel[]>>(`${environment.baseUrl}/api/customers`);
    }

    createCustomer(customer: CustomerCreationModel): Observable<DataResponseModel<CustomerModel>> {
        return this.httpClient.post<DataResponseModel<CustomerModel>>(`${environment.baseUrl}/api/customers`, customer);
    }
}