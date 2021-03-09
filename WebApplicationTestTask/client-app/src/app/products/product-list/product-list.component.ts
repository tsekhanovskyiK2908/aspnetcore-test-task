import { Component, OnInit } from '@angular/core';
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
  constructor(private productApiService: ProductsApiService) { }

  ngOnInit(): void {
    
    this.productApiService.getAllProducts().subscribe(response => {
      this.products = response.data;
    });
    
  }

  deleteProduct(productModel: ProductModel) {
    let result = confirm(`Do you want delete product: ${productModel.name}?`);

    if(result) {
      this.productApiService.deleteProduct(productModel.id).subscribe();
    }
  }
}