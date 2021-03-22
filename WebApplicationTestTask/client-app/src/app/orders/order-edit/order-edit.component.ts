import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomersApiService } from 'src/app/customers/api/customer-api-service';
import { CustomerModel } from 'src/app/customers/models/customer-model';
import { OrderApiService } from '../api/order-api-service';
import { OrderStatus } from '../enums/order-status';
import { OrderCreationalModel } from '../models/order-creational-model';
import { OrderPresentationModel } from '../models/order-presentation-model';
import { OrderProductModel } from '../models/order-product-model';
import { OrderProductPresentationModel } from '../models/order-product-presentation-model';
import { OrderStateModel } from '../models/order-state-model';
import { OrderUpdateModel } from '../models/order-update-model';
import { OrderStateService } from '../state/order-state-service';

@Component({
  selector: 'app-order-edit',
  templateUrl: './order-edit.component.html',
  styleUrls: ['./order-edit.component.scss']
})
export class OrderEditComponent implements OnInit {

  creationalMode: boolean;

  orderForm: FormGroup;
  orderModel: OrderPresentationModel;
  orderCreationalModel: OrderCreationalModel;
  orderStateModel: OrderStateModel;
  orderProductModels: OrderProductPresentationModel[];

  orderStatusKeys: string[];
  orderStatuses = OrderStatus;
  orderId: number;
  orderCreatedDate: number;
  totalCost: number;

  customers: CustomerModel[];

  constructor(private orderApiService: OrderApiService,
    private customerApiService: CustomersApiService,
    private route: ActivatedRoute, private router: Router,
    private orderStateService: OrderStateService) {
  }

  ngOnInit(): void {
    this.orderStatusKeys = Object.keys(this.orderStatuses).filter(f => !isNaN(Number(f)));
    this.orderId = +(this.route.snapshot.params['id']) || 0;

    if (this.orderId === 0) {
      this.creationalMode = true;
    }

    this.customerApiService.getAllCustomers().subscribe(dr => {
      this.customers = dr.data
    });

    this.orderForm = new FormGroup({
      customerId: new FormControl('', [Validators.required, Validators.min(1)]),
      orderStatus: new FormControl('', [Validators.required, Validators.min(1)]),
      comment: new FormControl('')
    });

    if (this.creationalMode) {
      if (this.orderStateService.stateIsAvaliable()) {
        console.log('create after adding product');
        this.orderStateModel = this.orderStateService.loadOrderState();
        this.orderForm.patchValue(this.orderStateModel);
        this.orderProductModels = this.orderStateModel.orderProducts;
        this.calculateOrdersTotalCost();
      } else {
        console.log('create clean');
        this.orderCreatedDate = Date.now();
        this.orderProductModels = new Array<OrderProductPresentationModel>();
      }
    } else {
      if (this.orderStateService.stateIsAvaliable()) {
        console.log('edit after adding product');
        this.orderStateModel = this.orderStateService.loadOrderState();
        this.orderForm.patchValue(this.orderStateModel);
        this.orderProductModels = this.orderStateModel.orderProducts;
        this.calculateOrdersTotalCost();
      } else {
        console.log('clean edit');
        this.orderApiService.getOrderById(this.orderId).subscribe(dr => {
          this.orderForm.patchValue({
            customerId: dr.data.customerId,
            orderStatus: dr.data.orderStatus,
            comment: dr.data.comment
          });

          this.orderCreatedDate = dr.data.orderDate;
          this.orderProductModels = dr.data.orderProducts;
          this.calculateOrdersTotalCost();
        });
      }
    }
  }

  saveOrder(): void {
    
    let orderProductModels = this.orderProductModels.map(op => {
      
      let orderProductModel: OrderProductModel = new OrderProductModel();
      orderProductModel.productId = op.productId;
      orderProductModel.quantity = op.quantity;

      return orderProductModel;
    });

    if(this.orderId !== 0) {
      let orderUpdateModel: OrderUpdateModel = this.orderForm.value;
      orderUpdateModel.id = this.orderId;
      orderUpdateModel.orderProductModels = orderProductModels;

      this.orderApiService.updateOrder(this.orderId, orderUpdateModel).subscribe(dr => {
        if(dr.data > 0) {
          this.orderStateService.disposeOrderState();
          this.router.navigate(['orders']);
        }
      });      
    } else {
      let orderCreationalModel: OrderCreationalModel = this.orderForm.value;        
      orderCreationalModel.orderProductModels = orderProductModels;

      this.orderApiService.createOrder(orderCreationalModel).subscribe(dr => {
        if (dr.data > 0) {
          this.orderStateService.disposeOrderState();
          this.router.navigate(['orders']);
        }
      });
    }
  }

  navigateToAddProducts(): void {
    if (this.orderForm.valid) {
      this.orderStateModel = this.orderForm.value;
      this.orderStateModel.orderProducts = this.orderProductModels;
      this.orderStateService.saveOrderState(this.orderStateModel);

      if (this.orderId !== 0) {
        this.router.navigate(['orders/add-product'], { queryParams: { orderId: this.orderId } });
      } else {
        this.router.navigate(['orders/add-product'], { queryParams: { newOrder: true } });
      }
    }
  }

  cancelEditingOrder(): void {
    this.orderStateService.disposeOrderState();
    this.router.navigate(['orders']);
  }

  calculateOrdersTotalCost(): void {
    let sum: number = 0;
    this.orderProductModels.forEach(op => sum += op.price);
    this.totalCost = sum;
  }

  get customerId() {
    return this.orderForm.get('customerId');
  }

  get orderStatus() {
    return this.orderForm.get('orderStatus');
  }
}
