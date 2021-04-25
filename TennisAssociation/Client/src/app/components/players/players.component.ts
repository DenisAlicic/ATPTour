import { PlayersService } from '../../services/players.service';
import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.scss'],
})
export class PlayersComponent implements OnInit {
  players$ = new BehaviorSubject([]);
  isLoading$ = new BehaviorSubject(true);

  constructor(private playersService: PlayersService) {}

  ngOnInit() {
    this.loadPlayers();
  }

  loadPlayers() {
    this.playersService.getPlayers().subscribe(value => {
      this.players$.next(value);
      this.isLoading$.next(false);
    });
  }
}
