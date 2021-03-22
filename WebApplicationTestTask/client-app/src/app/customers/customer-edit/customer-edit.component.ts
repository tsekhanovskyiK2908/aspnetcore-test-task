import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomersApiService } from '../api/customer-api-service';
import { CustomerCreationModel } from '../models/customer-creation-model';

@Component({
  selector: 'app-customer-edit',
  templateUrl: './customer-edit.component.html',
  styleUrls: ['./customer-edit.component.scss']
})
export class CustomerEditComponent implements OnInit {

  customerForm: FormGroup;
  customerCreationModel: CustomerCreationModel;
  customerCreatedDate: number;

  constructor(private customerApiService: CustomersApiService, 
    private router: Router) { }

  ngOnInit(): void {
    this.customerCreatedDate = Date.now();

    this.customerForm = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
      address: new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(100)])
    })
  }

  saveCustomer(): void {
    this.customerApiService.createCustomer(this.customerForm.value).subscribe(dr => {
      console.log(dr.data.name);
    });

    this.router.navigate(['/customers']);
  }

  get name() {
    return this.customerForm.get('name');
  }

  get address() {
    return this.customerForm.get('address');
  }

}
