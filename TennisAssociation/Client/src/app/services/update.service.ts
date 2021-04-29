import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class UpdateService {
  private readonly updateUrl = 'http://localhost:8080/api/updateData/';
  private readonly playersUrl = 'players';
  private readonly tournamentsUrl = 'tournaments';
  private readonly matchesUrl = 'matches';

  constructor(private http: HttpClient) { }

  updatePlayersDb() {
    return this.http.get(this.updateUrl + this.playersUrl);
  }

  updateTournamentsDb() {
    return this.http.get(this.updateUrl + this.tournamentsUrl);
  }

  updateMatchesDb() {
    return this.http.get(this.updateUrl + this.matchesUrl);
  }
}