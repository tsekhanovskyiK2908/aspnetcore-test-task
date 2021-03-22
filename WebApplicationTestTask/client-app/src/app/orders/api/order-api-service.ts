import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { DataResponseModel } from "src/app/shared/models/dataResponseModel";
import { environment } from "src/environments/environment";
import { OrderCreationalModel } from "../models/order-creational-model";
import { OrderPresentationModel } from "../models/order-presentation-model";
import { OrderUpdateModel } from "../models/order-update-model";

@Injectable()
export class OrderApiService {
    constructor(private httpClient: HttpClient) {}

    getOrders(): Observable<DataResponseModel<OrderPresentationModel[]>> {
        return this.httpClient.get<DataResponseModel<OrderPresentationModel[]>>(`${environment.baseUrl}/api/orders`);
    }

    getOrderById(orderId:number): Observable<DataResponseModel<OrderPresentationModel>> {
        return this.httpClient.get<DataResponseModel<OrderPresentationModel>>(`${environment.baseUrl}/api/orders/${orderId}`);
    }

    createOrder(orderModel: OrderCreationalModel): Observable<DataResponseModel<number>> {
        return this.httpClient.post<DataResponseModel<number>>(`${environment.baseUrl}/api/orders`, orderModel);
    }
    
    updateOrder(id:number, orderUpdateModel: OrderUpdateModel): Observable<DataResponseModel<number>> {
        return this.httpClient.put<DataResponseModel<number>>(`${environment.baseUrl}/api/orders/${id}`, orderUpdateModel);
    }
}