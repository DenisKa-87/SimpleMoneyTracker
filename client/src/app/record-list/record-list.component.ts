import { Component, OnInit } from '@angular/core';
import { RecordService } from '../services/record.service';
import { Record } from '../_models/record';
import { ToastrService } from 'ngx-toastr';
import { HttpResponse } from '@angular/common/http';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-record-list',
  templateUrl: './record-list.component.html',
  styleUrls: ['./record-list.component.css']
})
export class RecordListComponent implements OnInit {
  constructor(public recordService: RecordService, private toastr: ToastrService){}
  ngOnInit(): void {
    this.getRecords();
  }

  records: Record[];

  getRecords(){
    this.recordService.getRecords().subscribe(x => this.records = x);
    
  }

  deleteRecord(recordId: number){
    this.recordService.deleteRecord(recordId).subscribe(x => {
      this.toastr.warning(x.message)
    })
    this.recordService.records.splice(this.recordService.records.findIndex(m => m.id === recordId),1)
  }
}
