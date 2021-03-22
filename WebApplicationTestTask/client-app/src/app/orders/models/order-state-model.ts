import { OrderStatus } from "../enums/order-status";
import { OrderProductPresentationModel } from "./order-product-presentation-model";

export class OrderStateModel {
    customerId: number;
    orderStatus: OrderStatus;
    comment: string;
    totalCost: number;
    orderProducts: OrderProductPresentationModel[];
}