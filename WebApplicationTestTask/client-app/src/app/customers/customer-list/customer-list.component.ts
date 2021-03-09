import { Component, OnInit } from '@angular/core';
import { CustomersApiService } from '../api/customer-api-service';
import { CustomerModel } from '../models/customer-model';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.scss']
})
export class CustomerListComponent implements OnInit {

  customers: CustomerModel[];

  constructor(private customerApiService: CustomersApiService) { }

  ngOnInit(): void {
    this.customerApiService.getAllCustomers().subscribe(dataResponse => {
      this.customers = dataResponse.data;
    });
  }

}
