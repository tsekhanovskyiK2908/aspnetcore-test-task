import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderListComponent } from './order-list/order-list.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { OrderApiService } from './api/order-api-service';
import { OrderEditComponent } from './order-edit/order-edit.component';
import { CustomersApiService } from '../customers/api/customer-api-service';
import { OrderViewComponent } from './order-view/order-view.component';

const orderRoutes = [
  {path: 'orders', component: OrderListComponent },
  {path: 'orders/create', component: OrderEditComponent }
];

@NgModule({
  declarations: [OrderListComponent, OrderEditComponent, OrderViewComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(orderRoutes),
    ReactiveFormsModule
  ],
  providers: [OrderApiService, CustomersApiService]
})
export class OrdersModule { }
