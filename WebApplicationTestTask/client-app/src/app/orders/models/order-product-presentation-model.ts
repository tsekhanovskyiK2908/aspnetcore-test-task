import { ProductCategory } from "src/app/products/enums/product-category";

export class OrderProductPresentationModel {
    productId: number;
    productName: string;
    productCategory: ProductCategory;
    quantity: number;
    price: number;    
    productPrice: number;
}