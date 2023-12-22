import { Component, OnInit, ViewChild } from '@angular/core';
import { RecordService } from '../services/record.service';
import { Record } from '../_models/record';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-record',
  templateUrl: './add-record.component.html',
  styleUrls: ['./add-record.component.css']
})
export class AddRecordComponent implements OnInit {
  constructor(private recordService: RecordService, private toastr: ToastrService, private fb: FormBuilder){
  }
  ngOnInit(): void {
    this.initializeForm();
  }
  //@ViewChild('recordForm') 
  recordForm: FormGroup;
  maxDate = new Date();
  //record = new Record;


  addRecord(){
    this.recordService.addRecord(this.recordForm.value).subscribe(() => {
      this.toastr.success("The record has been added!")
      this.recordForm.reset();
    })
  }

  initializeForm(){
    this.recordForm = this.fb.group({
      name: new FormControl("", Validators.required),
      price: new FormControl("", Validators.required),
      category: new FormControl(""),
      description: new FormControl(""),
      date: new FormControl(""),
      recordType: new FormControl("1")
    })
  }

  getControl(control: any){
    return control as FormControl
  }

  
  
}
