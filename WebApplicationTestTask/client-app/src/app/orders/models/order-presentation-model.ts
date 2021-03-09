import { OrderStatus } from "../enums/order-status";

export class OrderPresentationModel {
    id: number;
    customerName: string;
    customerAddress: string;
    totalCost: number;
    orderStatus: OrderStatus
}