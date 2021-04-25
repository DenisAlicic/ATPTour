import { MatIconModule } from '@angular/material/icon';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavBarModule } from './../nav-bar/nav-bar.module';
import { CommonModule, LowerCasePipe, TitleCasePipe } from '@angular/common';
import { StatisticsComponent } from './statistics.component';
import { NgModule } from "@angular/core";
import { HttpClientModule } from '@angular/common/http';
import { ChartsModule } from 'ng2-charts';
import { PlayersBaseModule } from '../players/players-base/players-base.module';
import { IconModule } from 'src/app/shared/icon.module';

@NgModule({
  declarations: [ StatisticsComponent ], 
  imports: [
    CommonModule, 
    HttpClientModule,
    NavBarModule,
    ChartsModule,
    IconModule,
    MatIconModule,
    PlayersBaseModule
  ],
  exports: [ StatisticsComponent ],
  providers: [ TitleCasePipe, LowerCasePipe ]
})
export class StatisticsModule {}