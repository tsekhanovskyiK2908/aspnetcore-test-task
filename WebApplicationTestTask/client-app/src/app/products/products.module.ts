import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductListComponent } from './product-list/product-list.component';

import { HttpClientModule } from '@angular/common/http';
import { ProductsApiService } from './api/product-api-service';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { ProductViewComponent } from './product-view/product-view.component';
import { OrderApiService } from '../orders/api/order-api-service';
import { SharedModule } from '../shared/shared.module';

const productRoutes: Routes = [
  {path: 'products', component: ProductListComponent},
  {path: 'products/create', component: ProductEditComponent},
  {path: 'products/edit/:id', component: ProductEditComponent},
  {path: 'products/view/:id', component: ProductViewComponent}
];

@NgModule({
  declarations: [ProductListComponent, ProductEditComponent, ProductViewComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(productRoutes),
    ReactiveFormsModule,
    SharedModule
  ],
  providers: [ProductsApiService, OrderApiService],
  exports: []
})
export class ProductsModule { }
