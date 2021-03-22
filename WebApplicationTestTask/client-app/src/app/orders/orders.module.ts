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
import { OrderAddProductComponent } from './order-add-product/order-add-product.component';
import { ProductsApiService } from '../products/api/product-api-service';
import { OrderStateService } from './state/order-state-service';
import { SharedModule } from '../shared/shared.module';

const orderRoutes = [
  {path: 'orders', component: OrderListComponent },
  {path: 'orders/create', component: OrderEditComponent },
  {path: 'orders/view/:id', component: OrderViewComponent },
  {path: 'orders/edit/:id', component: OrderEditComponent },
  {path: 'orders/add-product', component: OrderAddProductComponent}
];

@NgModule({
  declarations: [OrderListComponent, OrderEditComponent, OrderViewComponent, OrderAddProductComponent],
  imports: [
    CommonModule,
    SharedModule,
    HttpClientModule,
    RouterModule.forChild(orderRoutes),
    ReactiveFormsModule
  ],
  providers: [OrderApiService, CustomersApiService, ProductsApiService, OrderStateService]
})
export class OrdersModule { }
