import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomersApiService } from 'src/app/customers/api/customer-api-service';
import { CustomerModel } from 'src/app/customers/models/customer-model';
import { OrderApiService } from '../api/order-api-service';
import { OrderStatus } from '../enums/order-status';

@Component({
  selector: 'app-order-edit',
  templateUrl: './order-edit.component.html',
  styleUrls: ['./order-edit.component.scss']
})
export class OrderEditComponent implements OnInit {

  orderForm: FormGroup;
  orderId: number;
  orderDate: number;

  orderStatusKeys: string[];
  orderStatuses = OrderStatus;

  customers: CustomerModel[];

  constructor(private orderApiService: OrderApiService, 
    private customerApiService: CustomersApiService) { 
      this.customerApiService.getAllCustomers().subscribe(dr => {
        this.customers = dr.data
      });
    
      this.orderStatusKeys = Object.keys(this.orderStatuses).filter(f => !isNaN(Number(f)));
  }

  ngOnInit(): void {
    this.orderId = 0;
    this.orderDate = Date.now();
    
    this.orderForm = new FormGroup({
      customer: new FormControl('', Validators.required),
      status: new FormControl('', Validators.required),
      comment: new FormControl('', Validators.required)      
    });
  }

  saveOrder(): void {

  }

}
