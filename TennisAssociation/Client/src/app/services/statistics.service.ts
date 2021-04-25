import { HandStatisticsModel } from './../models/statistics.model';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {
  private readonly statisticsUrl = 'http://localhost:8080/api/statistics/';

  private readonly handUrl = 'hand';

  constructor(private http: HttpClient) { }

  getHandStatistics() {
    return this.http.get<HandStatisticsModel[]>(this.statisticsUrl + this.handUrl);
  }
}