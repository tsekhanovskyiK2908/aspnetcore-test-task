import { ProductCategory } from "../enums/product-category";

export class ProductModel {
    id: number;
    name: string;
    productCategory: ProductCategory;
    avaliableQuantity: number;
    price: number;
    createdDate: number;
    description: string;
}