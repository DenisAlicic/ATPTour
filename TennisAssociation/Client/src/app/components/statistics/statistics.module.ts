import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavBarModule } from './../nav-bar/nav-bar.module';
import { CommonModule, TitleCasePipe } from '@angular/common';
import { StatisticsComponent } from './statistics.component';
import { NgModule } from "@angular/core";
import { HttpClientModule } from '@angular/common/http';
import { ChartsModule } from 'ng2-charts';

@NgModule({
  declarations: [ StatisticsComponent ], 
  imports: [
    CommonModule, 
    HttpClientModule,
    NavBarModule,
    ChartsModule
  ],
  exports: [ StatisticsComponent ],
  providers: [ TitleCasePipe ]
})
export class StatisticsModule {}