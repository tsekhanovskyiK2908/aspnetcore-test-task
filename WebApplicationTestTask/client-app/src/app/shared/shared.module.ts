import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EnumConverterPipe } from './pipes/enum-converter.pipe';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { MatDialogModule } from "@angular/material/dialog";
import { InformationDialogComponent } from './information-dialog/information-dialog.component';
import { ProductCategoryPipe } from './pipes/product-category.pipe';
import { ProductSizePipe } from './pipes/product-size.pipe';
import { OrderStatusPipe } from './pipes/order-status.pipe';

@NgModule({
  declarations: [EnumConverterPipe, ConfirmationDialogComponent, InformationDialogComponent, ProductCategoryPipe, ProductSizePipe, OrderStatusPipe],
  imports: [
    CommonModule,
    MatDialogModule,
  ],
  exports: [EnumConverterPipe, ProductCategoryPipe, ProductSizePipe, OrderStatusPipe]
})
export class SharedModule { }
