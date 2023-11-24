import { Component } from '@angular/core';
import { ModesService } from '../services/modes.service';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor(private mode: ModesService, public accountService: AccountService){
  }

  signInMode=false;
  toggleSignInMode(){
    //this.mode.registrationMode = true;
    this.signInMode = true;
  }

  exitSignInMode(event: boolean){
    this.mode.registrationMode = event;
    this.signInMode = event;
  }

}
