import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {BsDropdownModule} from 'ngx-bootstrap/dropdown';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';
import { AddRecordComponent } from './add-record/add-record.component';
import { SigninComponent } from './signin/signin.component';
import {  ReactiveFormsModule, FormControl, FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { ErrorsInterceptor } from './_interceptors/errors.interceptor';
import { JwtTokenInterceptor } from './_interceptors/jwt-token.interceptor';
import { RecordListComponent } from './record-list/record-list.component';
import { DatePickerComponent } from './_interceptors/_forms/date-picker/date-picker.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { PaginationModule } from 'ngx-bootstrap/pagination';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavComponent,
    AddRecordComponent,
    SigninComponent,
    RecordListComponent,
    DatePickerComponent,
    
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(
      {positionClass: 'toast-bottom-right'}),
    FormsModule,
    BsDropdownModule.forRoot(), 
    BsDatepickerModule.forRoot(),
    PaginationModule.forRoot()
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorsInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtTokenInterceptor, multi: true},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
