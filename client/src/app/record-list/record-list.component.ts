import { Component, OnInit } from '@angular/core';
import { RecordService } from '../services/record.service';
import { Record } from '../_models/record';
import { ToastrService } from 'ngx-toastr';
import { HttpResponse } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { UserParams } from '../_models/userParams';
import { Pagination } from '../_models/Pagination';
import { Category } from '../_models/Category';
import {BsModalRef, BsModalService, ModalOptions} from 'ngx-bootstrap/modal'
import { RecordcardComponent } from '../recordcard/recordcard.component';

@Component({
  selector: 'app-record-list',
  templateUrl: './record-list.component.html',
  styleUrls: ['./record-list.component.css']
})
export class RecordListComponent implements OnInit {

  records: Record[]
  userParams: UserParams;
  pagination: Pagination;
  categories: Category[] = []
  sortingList = [{value: 'date', display: 'Date'}, {value: 'value', display: 'Price'}, 
  {value: 'category', display: 'Category'} ]
  bsModalRef?: BsModalRef;

  constructor(public recordService: RecordService, private toastr: ToastrService, private modalService: BsModalService){
    this.userParams = recordService.getUserParams();
  }
  ngOnInit(): void {
    this.getCategories();
    this.getRecords();
  }
  getCategories(){
    this.recordService.getCategories().subscribe(x => this.categories = x)
  }

  getRecords(){
    console.log(this.userParams)
    this.recordService.setUserParams(this.userParams)
    this.recordService.getRecords(this.userParams).subscribe(x => {
      this.records = x.result;
      this.pagination = x.pagination;
    }); 
  }

  editRecord(record: Record){
    const initialState: ModalOptions = {
      initialState: {
        record: record,
        title: 'Edit record'
      }
    };
    this.bsModalRef = this.modalService.show(RecordcardComponent, initialState);
    this.bsModalRef.content.bsModalRef = this.bsModalRef;

  }

  deleteRecord(recordId: number){
    this.recordService.deleteRecord(recordId).subscribe(x => {
      this.toastr.warning(x.message)
    })
  }

  pageChanged(event: any){
    this.userParams.pageNumber = event.page
    this.recordService.setUserParams(this.userParams)
    this.getRecords()
  }

  

}
