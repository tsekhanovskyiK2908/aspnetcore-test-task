import { Injectable } from "@angular/core";
import { OrderCreationalModel } from "../models/order-creational-model";
import { OrderProductModel } from "../models/order-product-model";
import { OrderProductPresentationModel } from "../models/order-product-presentation-model";
import { OrderStateModel } from "../models/order-state-model";


@Injectable({
    providedIn: 'root'
})
export class OrderStateService {
    private orderModel: OrderStateModel;// = new OrderStateModel();        

    // constructor() {
    //     this.orderModel = new OrderStateModel();
    //     this.orderModel.orderProducts = Array<OrderProductPresentationModel>();
    // }

    saveOrderState(orderModel: OrderStateModel): void {
        this.orderModel = orderModel;
        if(this.orderModel.orderProducts === undefined) {
            this.orderModel.orderProducts = new Array<OrderProductPresentationModel>();
        }
        console.log('order model: ');
        console.log(orderModel);
    }

    loadOrderState(): OrderStateModel {
        return this.orderModel;
    }

    addProductToOrder(orderProduct: OrderProductPresentationModel): void {        
        let orderInArray: OrderProductPresentationModel = this.orderModel.orderProducts
                                                                .find(op => op.productId === orderProduct.productId);

        if(orderInArray === undefined) {
            this.orderModel.orderProducts.push(orderProduct);
        } else {
            this.orderModel.orderProducts = this.orderModel.orderProducts.map(product => {               
                if(product.productId === orderProduct.productId) {
                    product.quantity += orderProduct.quantity;
                    product.price += orderProduct.price;
                }

                return product;
            });
        } 

        console.log(this.orderModel);
    }

    disposeOrderState(): void {
        this.orderModel = undefined;
    }

    stateIsAvaliable(): boolean {
        return this.orderModel != undefined;
    }
}

