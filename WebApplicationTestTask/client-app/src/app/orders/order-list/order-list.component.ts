import { Component, OnInit } from '@angular/core';
import { OrderApiService } from '../api/order-api-service';
import { OrderPresentationModel } from '../models/order-presentation-model';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss']
})
export class OrderListComponent implements OnInit {

  orders: OrderPresentationModel[];
  constructor(private orderApiService : OrderApiService) { }

  ngOnInit(): void {
    setTimeout(() => {
      this.orderApiService.getOrders().subscribe(dr => {
        this.orders = dr.data;
      }),100
    });
  }

}
