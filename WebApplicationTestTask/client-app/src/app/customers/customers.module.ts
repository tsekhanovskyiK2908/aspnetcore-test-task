import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { CustomerEditComponent } from './customer-edit/customer-edit.component';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CustomersApiService } from './api/customer-api-service';
import { ReactiveFormsModule } from '@angular/forms';

const customerRoutes: Routes = [
  {path: 'customers', component: CustomerListComponent},
  {path: 'customers/create', component: CustomerEditComponent}
];

@NgModule({
  declarations: [CustomerListComponent, CustomerEditComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(customerRoutes),
    ReactiveFormsModule
  ],
  providers: [CustomersApiService]
})
export class CustomersModule { }
