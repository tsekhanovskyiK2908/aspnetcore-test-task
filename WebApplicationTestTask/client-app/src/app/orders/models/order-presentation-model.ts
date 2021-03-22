import { OrderStatus } from "../enums/order-status";
import { OrderProductPresentationModel } from "./order-product-presentation-model";

export class OrderPresentationModel {
    id: number;
    customerId: number;
    customerName: string;
    customerAddress: string;
    totalCost: number;
    orderStatus: OrderStatus
    orderProducts: OrderProductPresentationModel[];
    orderDate: number;
    comment: string;
}