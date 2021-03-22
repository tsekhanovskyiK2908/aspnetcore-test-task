import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'productSize'
})
export class ProductSizePipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    return null;
  }

}
