import { StatisticsModule } from './components/statistics/statistics.module';
import { SignupModule } from './components/signup/signup.module';
import { LoginModule } from './components/login/login.module';
import { NavBarModule } from './components/nav-bar/nav-bar.module';
import { PlayersModule } from './components/players/players.module';
import { TournamentsModule } from './components/tournaments/tournaments.module';
import { MatchesModule } from './components/matches/matches.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { MatTableModule } from '@angular/material/table';
import { MatButtonModule} from '@angular/material/button';
import { ChartsModule } from 'ng2-charts';


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    ReactiveFormsModule,
    HttpClientModule,
    PlayersModule,
    TournamentsModule,
    MatchesModule,
    NavBarModule, 
    LoginModule,
    SignupModule,
    StatisticsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
