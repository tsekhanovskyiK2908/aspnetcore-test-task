import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductsApiService } from 'src/app/products/api/product-api-service';
import { ProductModel } from 'src/app/products/models/product-model';
import { InformationDialogComponent } from 'src/app/shared/information-dialog/information-dialog.component';
import { OrderProductModel } from '../models/order-product-model';
import { OrderProductPresentationModel } from '../models/order-product-presentation-model';
import { OrderStateService } from '../state/order-state-service';

@Component({
  selector: 'app-order-add-product',
  templateUrl: './order-add-product.component.html',
  styleUrls: ['./order-add-product.component.scss']
})
export class OrderAddProductComponent implements OnInit {

  productModels: ProductModel[];
  orderId: number;
  selectedProductModel: ProductModel;

  addProductForm: FormGroup;

  constructor(private productApiService: ProductsApiService,
    private route: ActivatedRoute,
    private orderStateService: OrderStateService,
    private router: Router,
    private informationDialog: MatDialog) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.orderId = +params['orderId'] || 0;
    });

    this.addProductForm = new FormGroup({
      productId: new FormControl('', [Validators.required, Validators.min(1)]),
      quantity: new FormControl('', [Validators.required, Validators.min(1)])
    });

    //TODO 
    // 404 handling

    this.productApiService.getAllProducts().subscribe(dr => {
      this.productModels = dr.data;
    })
  }

  onSelectedProductChange(productId: number) {
    if(productId !== null) {
      // === does not work. Why?
      this.selectedProductModel = this.productModels.find(p => p.id == productId);
    }
  }

  addProductToOrder():void {
    let productToAdd:OrderProductModel = this.addProductForm.value;    

    if(productToAdd.quantity > this.productModels.find(p => p.id == productToAdd.productId).avaliableQuantity) {
      this.informationDialog.open(InformationDialogComponent, {
        data: {message: "Quantity you ordered is less than avaliable"}
      });
    } else {
      let orderProductPresentationModel = new OrderProductPresentationModel();

      orderProductPresentationModel.productId = this.selectedProductModel.id;
      orderProductPresentationModel.productCategory = this.selectedProductModel.productCategory;
      orderProductPresentationModel.productName = this.selectedProductModel.name;
      orderProductPresentationModel.quantity = +productToAdd.quantity;
      orderProductPresentationModel.price = this.selectedProductModel.price*(+productToAdd.quantity);

      this.orderStateService.addProductToOrder(orderProductPresentationModel);

      this.backToEditPage();
      
    }
  }

  backToEditPage():void {
    if (this.orderId === 0) {
      this.router.navigateByUrl('/orders/create');
    } else {
      this.router.navigateByUrl(`/orders/edit/${this.orderId}`);
    }
  }

  get productId() {
    return this.addProductForm.get('productId');
  }

  get quantity() {
    return this.addProductForm.get('quantity');
  }
}
