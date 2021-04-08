import { MatchModel } from '../models/matches.model';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class MatchesService {
  private readonly matchesUrl = 'http://localhost:8080/api/matches/';

  constructor(private http: HttpClient) { }

  getMatches() {
    return this.http.get<MatchModel[]>(this.matchesUrl);
  }
}
