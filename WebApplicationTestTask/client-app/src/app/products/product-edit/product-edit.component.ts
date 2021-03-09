import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProductsApiService } from '../api/product-api-service';
import { ProductCategory } from '../enums/product-category';
import { ProductCreationModel } from '../models/product-creation-model';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductModel } from '../models/product-model';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.scss']
})

export class ProductEditComponent implements OnInit {

  creationMode: boolean;

  categories = ProductCategory;
  categoriesKeys: string[];
  productForm: FormGroup;
  productCreationModel: ProductCreationModel;

  productCreatedDate: number;
  productId: number;
  productModel: ProductModel;

  constructor(private productApiService: ProductsApiService,
    private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.categoriesKeys = Object.keys(this.categories).filter(Number);
    this.productId = +(this.route.snapshot.params['id']);

    if (isNaN(this.productId)) {
      this.creationMode = true;
      this.productId = 0;
      this.productCreatedDate = Date.now();
    }

    this.productForm = new FormGroup({
      id: new FormControl({value: '', disabled: true}),
      name: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(255)]),
      productCategory: new FormControl('', [Validators.required]),
      avaliableQuantity: new FormControl('', [Validators.required, Validators.min(0)]),
      price: new FormControl('', [Validators.required, Validators.min(0.01)]),
      description: new FormControl('')
    });

    if (!this.creationMode) {
      this.productApiService.getProduct(this.productId).subscribe(dr => {
        this.productModel = dr.data;
      });

      setTimeout(() => {
        this.productForm.patchValue(this.productModel);
        this.productCreatedDate = this.productModel.createdDate;
      }, 100);

      console.log(this.productForm);
    } else {
      this.productForm.patchValue({id: this.productId});
    }
  }

  saveProduct(): void {
    let saved:boolean = false;

    if (this.creationMode) {
      this.productCreationModel = this.productForm.value;
      this.productApiService.createProduct(this.productCreationModel).subscribe(dr => {
      })
    } else {
      this.productModel = this.productForm.value;
      this.productApiService.updateProduct(this.productId, this.productModel).subscribe(r => {      
      });
    }

    this.router.navigate(['products']);
  }
}
