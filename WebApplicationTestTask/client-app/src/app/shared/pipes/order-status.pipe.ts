import { Pipe, PipeTransform } from '@angular/core';
import { OrderStatus } from 'src/app/orders/enums/order-status';

@Pipe({
  name: 'orderStatus'
})
export class OrderStatusPipe implements PipeTransform {

  transform(key: number): string {
    switch (key) {
      case OrderStatus.New:
        return "New";
      case OrderStatus.Paid:
        return "Paid";
      case OrderStatus.Shipped:
        return "Shipped";
      case OrderStatus.Delivered:
        return "Delivered";
      case OrderStatus.Closed:
        return "Closed";
      default:
        break;
    }
  }

}
