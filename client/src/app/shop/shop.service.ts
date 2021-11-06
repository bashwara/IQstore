import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { ProductBrand } from '../shared/models/productBrand';
import { ProductCategory } from '../shared/models/productCategory';
import { map } from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';
import { Product } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams) {

    let params = new HttpParams();

    if(shopParams.brandId !== 0) {
      params = params.append('brandId', shopParams.brandId.toString());
    }
    
    if(shopParams.categoryId !== 0) {
      params = params.append('categoryId', shopParams.categoryId.toString());
    }

    if(shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());

    return this.http.get<Pagination>(this.baseUrl + 'products', {observe: 'response', params})
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }

  getProduct(id: number) {
    return this.http.get<Product>(this.baseUrl + 'products/' + id);
  }

  getBrands() {
    return this.http.get<ProductBrand[]>(this.baseUrl + 'products/brands');
  }

  getCategories() {
    return this.http.get<ProductCategory[]>(this.baseUrl + 'products/categories');
  }
}
