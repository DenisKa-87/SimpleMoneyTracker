import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ModesService {

  registrationMode = false;
  addRecordMode = false;
  
  constructor() { }
}
