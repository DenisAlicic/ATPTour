import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { NgModule } from "@angular/core";
import { AppRoutingModule } from 'src/app/app-routing.module';
import { SettingsComponent } from "./settings.component";
import { NavBarModule } from '../nav-bar/nav-bar.module';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [ SettingsComponent ],
  imports: [
    CommonModule, 
    AppRoutingModule, 
    BrowserModule,
    NavBarModule,
    FormsModule, 
    ReactiveFormsModule,
    HttpClientModule
  ],
  exports: [ SettingsComponent ]
})
export class SettingsModule {}