import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderApiService } from '../api/order-api-service';
import { OrderPresentationModel } from '../models/order-presentation-model';

@Component({
  selector: 'app-order-view',
  templateUrl: './order-view.component.html',
  styleUrls: ['./order-view.component.scss']
})
export class OrderViewComponent implements OnInit {

  orderModel: OrderPresentationModel;
  orderModelDate: number;
  orderId: number;

  constructor(private orderApiService: OrderApiService,
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.orderModelDate = Date.now();
    this.orderId = +(this.route.snapshot.params['id']);

    if(isNaN(this.orderId)){
      this.router.navigate(['/orders']);
    }

    this.orderApiService.getOrderById(this.orderId).subscribe(dr => {
      this.orderModel = dr.data
      console.log(this.orderModel);
    });
  }

}
