import { TournamentModel } from '../models/tournaments.model';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class TournamentsService {
  private readonly tournamentsUrl = 'http://localhost:8080/api/tournaments/';

  constructor(private http: HttpClient) { }

  getTournaments() {
    return this.http.get<TournamentModel[]>(this.tournamentsUrl);
  }
}
