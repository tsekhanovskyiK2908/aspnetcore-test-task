import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { DataResponseModel } from "src/app/shared/models/dataResponseModel";
import { environment } from "src/environments/environment";
import { OrderPresentationModel } from "../models/order-presentation-model";

@Injectable()
export class OrderApiService {
    constructor(private httpClient: HttpClient) {}

    getOrders(): Observable<DataResponseModel<OrderPresentationModel[]>> {
        return this.httpClient.get<DataResponseModel<OrderPresentationModel[]>>(`${environment.baseUrl}/api/orders`);
    }
}