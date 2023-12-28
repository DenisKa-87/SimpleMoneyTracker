import { Component, Input, OnInit } from '@angular/core';
import { RecordService } from '../services/record.service';
import { ToastrService } from 'ngx-toastr';
import { Record } from '../_models/record';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-recordcard',
  templateUrl: './recordcard.component.html',
  styleUrls: ['./recordcard.component.css']
})
export class RecordcardComponent implements OnInit{
  constructor(private recordsService: RecordService, private toastr: ToastrService, private fb: FormBuilder){
  }
  ngOnInit(): void {
    this.initializeForm();
  }
  @Input()record: Record
  @Input()bsModalRef: BsModalRef

  editForm: FormGroup

  initializeForm(){
    this.editForm = this.fb.group({
      Name: new FormControl(this.record.Name),
      Price: new FormControl(this.record.Price),
      Category: new FormControl(this.record.Category),
      Description: new FormControl(this.record.Description),
      Date: new FormControl(this.record.Date),
      RecordType: new FormControl(this.record.RecordType)
    })
  }

  editRecord(){
    console.log(this.editForm.value)
    var res = this.editForm.value;
    this.record.Category = res.Category
    this.record.Name = res.Name
    this.record.Date = res.Date
    this.record.Price = res.Price
    this.record.RecordType = res.RecordType
    this.record.Description = res.Description

    this.recordsService.udpateRecord(this.record).subscribe(() => this.toastr.success("success"))
  }



}
