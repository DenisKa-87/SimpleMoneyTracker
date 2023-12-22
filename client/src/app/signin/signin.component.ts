import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from '../services/account.service';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators, FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ModesService } from '../services/modes.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit{
  /**
   *
   */

  registerForm: FormGroup;
  validationErrors: string[] = [];
  

  constructor(private accountService: AccountService, private fb: FormBuilder, private toastr: ToastrService,
     private modes: ModesService) { 
  }
  ngOnInit(): void {
    this.initializeForm();
  }

  matchValues(matchTo : string) : ValidatorFn{
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value ? null : {isMatching: true}
    }
  }
  initializeForm(){
    this.registerForm = this.fb.group({
      // email: ["", Validators.required],
      // name: ["", [Validators.required, Validators.minLength(2), Validators.maxLength(16)]],
      // password: ["", [Validators.required, Validators.minLength(8), Validators.maxLength(16)]],
      // confirmPassword: ["", [Validators.required, this.matchValues('password')]]
      email: new FormControl("", Validators.required),
      name: new FormControl("", [Validators.required, Validators.minLength(2), Validators.maxLength(16)]),
      password: new FormControl("", [Validators.required, Validators.minLength(8), Validators.maxLength(16)]),
      confirmPassword: new FormControl("", [Validators.required, this.matchValues('password')])
    })
  }

  signIn(){
    this.accountService.signIn(this.registerForm.value).subscribe({
      next: () => this.toastr.success("You have signed in! Please log in now."),
      error: error => this.validationErrors = error
    })
    this.cancel();
  }

  cancel(){
    this.modes.signInModeOff();

  }
}
