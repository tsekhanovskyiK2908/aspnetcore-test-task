import { ResponseModel } from "./responseModel";

export class DataResponseModel<T> extends ResponseModel {
    data: T;
}