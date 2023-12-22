import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Record } from '../_models/record';

@Injectable({
  providedIn: 'root'
})
export class RecordService {

  constructor(private http: HttpClient) { }
  records: Record[] = []
  
  getRecords(){
    return this.http.get<Record[]>(environment.apiUrl + "records")
  }

  addRecord(record: Record){
    return this.http.post<Record>(environment.apiUrl + "records/add", record)
  }

  deleteRecord(recordId: Number): any{
    return this.http.delete(environment.apiUrl + "records/" + recordId)
  }
}
