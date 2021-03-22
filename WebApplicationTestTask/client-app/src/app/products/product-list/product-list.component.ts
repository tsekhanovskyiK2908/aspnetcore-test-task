import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { OrderApiService } from 'src/app/orders/api/order-api-service';
import { OrderPresentationModel } from 'src/app/orders/models/order-presentation-model';
import { ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { InformationDialogComponent } from 'src/app/shared/information-dialog/information-dialog.component';
import { ProductsApiService } from '../api/product-api-service';
import { ProductModel } from '../models/product-model';
import { ProductsModule } from '../products.module';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {

  products: ProductsModule[];

  constructor(private productApiService: ProductsApiService,
    private orderApiService: OrderApiService, 
    private confirmationDialog: MatDialog,
    private informationDialog: MatDialog) { }

  ngOnInit(): void {

    this.productApiService.getAllProducts().subscribe(response => {
      this.products = response.data;
    });
  }

  deleteProduct(productModel: ProductModel) {
    let orders = [];
    
    this.orderApiService.getOrders().subscribe(response => {
      orders = response.data;
    });

    const dialogRef = this.confirmationDialog.open(ConfirmationDialogComponent, {
      data: {message: `Do you want to delete product: ${productModel.name}?`}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (orders.find(o => o.orderProducts
                        .find(op => op.productId === productModel.id)) === undefined) {          
          this.productApiService.deleteProduct(productModel.id).subscribe();          
        } else {
          this.informationDialog.open(InformationDialogComponent, {
            data: {message: `${productModel.name} can't be deleted. It used in some orders`}
          });          
        }
      }
    });
  }
}