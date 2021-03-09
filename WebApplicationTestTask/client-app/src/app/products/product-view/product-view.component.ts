import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductsApiService } from '../api/product-api-service';
import { ProductModel } from '../models/product-model';

@Component({
  selector: 'app-product-view',
  templateUrl: './product-view.component.html',
  styleUrls: ['./product-view.component.scss']
})
export class ProductViewComponent implements OnInit {

  productModel: ProductModel;
  productId: number;

  constructor(private productsApiService: ProductsApiService, 
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.productId = +(this.route.snapshot.params['id']);

    if(isNaN(this.productId)) {
      this.router.navigate(['/products/']);
    }

    this.productsApiService.getProduct(this.productId).subscribe(dr => {
      this.productModel = dr.data;
    })
  }

}
