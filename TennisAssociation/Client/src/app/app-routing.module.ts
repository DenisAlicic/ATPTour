import { PlayersComponent } from './components/players/players.component';
import { TournamentsComponent } from './components/tournaments/tournaments.component';
import { MatchesComponent } from './components/matches/matches.component';
import { SignupComponent } from './components/signup/signup.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'players', component: PlayersComponent },
  { path: 'tournaments', component: TournamentsComponent },
  { path: 'matches', component: MatchesComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
