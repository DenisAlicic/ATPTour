import { HandStatisticsModel, HeightsStatisticsModel, YearsStatisticsModel } from './../models/statistics.model';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {
  private readonly statisticsUrl = 'http://localhost:8080/api/statistics/';

  private readonly handUrl = 'hand';
  private readonly heightUrl = 'heights';
  private readonly yearUrl = 'years';

  constructor(private http: HttpClient) { }

  getHandStatistics() {
    return this.http.get<HandStatisticsModel[]>(this.statisticsUrl + this.handUrl);
  }

  getHeightsStatistics() {
    return this.http.get<HeightsStatisticsModel[]>(this.statisticsUrl + this.heightUrl);
  }

  getYearsStatistics() {
    return this.http.get<YearsStatisticsModel[]>(this.statisticsUrl + this.yearUrl);
  }
}