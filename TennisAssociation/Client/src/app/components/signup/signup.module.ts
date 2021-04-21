import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './../../app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SignupComponent } from './signup.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [ SignupComponent ],
  imports: [ 
    CommonModule, 
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserModule,
    FormsModule,
  ],
  exports: [ SignupComponent ]
})
export class SignupModule {}

