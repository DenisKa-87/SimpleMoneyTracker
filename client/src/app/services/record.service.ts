import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Record } from '../_models/record';
import { UserParams } from '../_models/userParams';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { Category } from '../_models/Category';

@Injectable({
  providedIn: 'root'
})
export class RecordService {

  constructor(private http: HttpClient) { }
  //records: Record[] = []
  userParams = new UserParams();
  //categories: Category[];

  getUserParams() {
    return this.userParams;
  }

  setUserParams(params: UserParams) {
    this.userParams = params;
  }

  getCategories() {
    return this.http.get<Category[]>(environment.apiUrl + 'records/categories')
    // .subscribe(x => {
    // this.categories = x
    // })
    // console.log(this.categories)
    // return this.categories
  }

  getRecords(userParams: UserParams) {
    let params = getPaginationHeaders(userParams.pageNumber, userParams.itemsPerPage);
    params = params.append('categoryId', userParams.CategoryId);
    params = params.append('maxDate', userParams.MaxDate.toISOString());
    params = params.append('minDate', userParams.MinDate.toISOString());
    params = params.append('order', userParams.Order);
    params = params.append('recordType', userParams.RecordType);
    console.log("///////////")
    console.log(params)

    return getPaginatedResult<Record[]>(environment.apiUrl + 'records', params, this.http)
    //return this.http.get<Record[]>(environment.apiUrl + "records")
  }

  addRecord(record: Record) {
    return this.http.post<Record>(environment.apiUrl + "records/add", record)
  }

  udpateRecord(record: Record): any{
    return this.http.put(environment.apiUrl + 'records/'+record.Id, record)
  }

  deleteRecord(recordId: Number): any {
    return this.http.delete(environment.apiUrl + "records/" + recordId)
  }



}
