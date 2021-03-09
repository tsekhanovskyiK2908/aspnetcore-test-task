import { Component, OnInit } from '@angular/core';
import { OrderApiService } from '../api/order-api-service';
import { OrderPresentationModel } from '../models/order-presentation-model';

@Component({
  selector: 'app-order-view',
  templateUrl: './order-view.component.html',
  styleUrls: ['./order-view.component.scss']
})
export class OrderViewComponent implements OnInit {

  orderPresentationModel: OrderPresentationModel;
  constructor(private orderApiService: OrderApiService) { }

  ngOnInit(): void {
    this.orderApiService.getOrders();
  }

}
