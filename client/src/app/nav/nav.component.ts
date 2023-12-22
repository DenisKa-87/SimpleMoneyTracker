import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ModesService } from '../services/modes.service';
import { AccountService } from '../services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private modes: ModesService, public accountService: AccountService, private toastr: ToastrService){

  }
  ngOnInit(): void {
  }
  

  login(){
    this.accountService.logIn(this.model).subscribe({
      next: () =>  this.toastr.success("Successfully logged in!")
    }
    )
    this.modes.signInModeOff();
  }

  logout(){
    if (confirm("Do you really want to log out?")){
      this.accountService.logout();
      this.modes.signInModeOff();
    }
    else
      return;
  }
}
