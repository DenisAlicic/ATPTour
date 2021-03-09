import { AppRoutingModule } from './../../app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { NavBarModule } from '../nav-bar/nav-bar.module';
import { HomeComponent } from './home.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [ HomeComponent ],
  imports: [ 
    CommonModule,
    NavBarModule, 
    HttpClientModule, 
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule
  ],
  exports: [ HomeComponent ]
})
export class HomeModule {}

