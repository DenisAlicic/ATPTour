import { SettingsComponent } from './components/settings/settings.component';
import { PlayersComponent } from './components/players/players.component';
import { TournamentsComponent } from './components/tournaments/tournaments.component';
import { MatchesComponent } from './components/matches/matches.component';
import { SignupComponent } from './components/signup/signup.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { Pages } from './shared/pages';
import { LoginActivate } from './helpers/login.guard';
import { StatisticsComponent } from './components/statistics/statistics.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: Pages.Login, component: LoginComponent},
  { path: Pages.SignUp, component: SignupComponent },
  { path: Pages.Players, component: PlayersComponent, canActivate: [LoginActivate] },
  { path: Pages.Tournaments, component: TournamentsComponent, canActivate: [LoginActivate] },
  { path: Pages.Matches, component: MatchesComponent, canActivate: [LoginActivate] },
  { path: Pages.Statistics, component: StatisticsComponent, canActivate: [LoginActivate] },
  { path: Pages.Settings, component: SettingsComponent, canActivate: [LoginActivate]},
  { path: '**', redirectTo: Pages.Players, canActivate: [LoginActivate] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
