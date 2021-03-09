import { ProductCategory } from "../enums/product-category";

export class ProductCreationModel {
    name: string;
    productCategory: ProductCategory;
    avaliableQuantity: number;
    price: number;
    description: string;
}