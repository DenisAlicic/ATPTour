import { MatIconModule } from '@angular/material/icon';
import { AppRoutingModule } from '../../app-routing.module';
import { NavBarComponent } from './nav-bar.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [ NavBarComponent ],
  imports: [ CommonModule, AppRoutingModule, MatIconModule ],
  exports: [ NavBarComponent ]
})
export class NavBarModule {}

