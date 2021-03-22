import { Pipe, PipeTransform } from '@angular/core';
import { ProductCategory } from 'src/app/products/enums/product-category';

@Pipe({
  name: 'productCategory'
})
export class ProductCategoryPipe implements PipeTransform {

  transform(key: number): string {
    switch (key) {
      case ProductCategory.Foods:
        return 'Foods';
      case ProductCategory.Clothes:
        return 'Paid';
      case ProductCategory.Appliance:
        return 'Appliance';
      default:
        break;
    }
  }

}
