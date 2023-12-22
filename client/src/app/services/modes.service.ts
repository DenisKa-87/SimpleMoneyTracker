import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ModesService {

  public signInMode = new Subject<boolean>();
  public addRecordMode = new Subject<boolean>();
  
  constructor() { }

  signInModeOn(){
    this.signInMode.next(true)
  }

  signInModeOff(){
    this.signInMode.next(false)
  }
  addRecordModeOn(){
    this.addRecordMode.next(true)
  }
  addRecordModeOff(){
    this.addRecordMode.next(false)
  }
}
