import { PlayerInterface } from '../models/players.database.model';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class PlayersService {
  private readonly playersUrl = 'http://localhost:8080/api/players/';

  constructor(private http: HttpClient) { }

  getPlayers() {
    return this.http.get<PlayerInterface[]>(this.playersUrl);
  }

  getPlayerById(id: string) {
    return this.http.get<PlayerInterface>(this.playersUrl + id);
  }
}
