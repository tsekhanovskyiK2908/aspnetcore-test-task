import { OrderStatus } from "../enums/order-status";
import { OrderProductModel } from "./order-product-model";

export class OrderCreationalModel {
    customerId: number;
    orderStatus: OrderStatus;
    comment: string;
    orderProductModels: OrderProductModel[];
}