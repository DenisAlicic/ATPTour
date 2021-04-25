import { PlayerModel } from '../models/players.model';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class PlayersService {
  private readonly playersUrl = 'http://localhost:8080/api/players/';
  private readonly handUrl = 'hand/';

  constructor(private http: HttpClient) { }

  getPlayers() {
    return this.http.get<PlayerModel[]>(this.playersUrl);
  }

  getPlayerById(id: string) {
    return this.http.get<PlayerModel>(this.playersUrl + id);
  }

  getPlayersByHand(hand: string) {
    return this.http.get<PlayerModel[]>(this.playersUrl + this.handUrl + hand);
  }
}
