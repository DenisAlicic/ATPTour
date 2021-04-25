import { PlayersService } from './../../services/players.service';
import { LowerCasePipe, TitleCasePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { StatisticsService } from 'src/app/services/statistics.service';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.scss']
})
export class StatisticsComponent implements OnInit {
  // Hand Chart Pie
  handPieChartLabels$ = new BehaviorSubject([]);
  handPieChartData$ = new BehaviorSubject([]);
  handPieOptions: any = {
    legend: { position: 'left' }
  }
  handPieChartColors: Array<any> = [{
    backgroundColor: ['#ac97c1', '#71839d'],
  }];

  showStatistics$ = new BehaviorSubject(true);

  showPlayersByHand$ = new BehaviorSubject(false);
  arePlayersLoading$ = new BehaviorSubject(false);
  players$ = new BehaviorSubject([]);
  playersTitle$ = new BehaviorSubject('');

  constructor(
    private statisticsService: StatisticsService,
    private playersService: PlayersService,
    private titleCasePipe: TitleCasePipe,
    private lowerCasePipe: LowerCasePipe,
  ) {}

  ngOnInit() {
    this.loadHandStatistics();
  }

  loadHandStatistics() {
    this.statisticsService.getHandStatistics().subscribe(value => {
      this.handPieChartLabels$.next([this.getLabel(value[0].hand), this.getLabel(value[1].hand)]);
      this.handPieChartData$.next([value[0].count, value[1].count]);
    });
  }

  getLabel(hand: string) {
    return this.titleCasePipe.transform(hand + ' Handed'); 
  }

  getLabelFromActiveChart(chart: any) {
    return chart.active[0]._model.label;
  }

  getHandFromLabel(chart: any) {
    return this.lowerCasePipe.transform(this.getLabelFromActiveChart(chart).split(' ')[0]);
  }

  public handChartClicked(e:any) {
    this.showStatistics$.next(false);
    this.arePlayersLoading$.next(true);
    this.playersTitle$.next(this.getLabelFromActiveChart(e) + ' Players');
    const hand = this.getHandFromLabel(e);
    this.playersService.getPlayersByHand(hand).subscribe(value => {
      this.players$.next(value);
      this.arePlayersLoading$.next(false);
      this.showPlayersByHand$.next(true);
    });
  }

  backToStatistics() {
    this.showStatistics$.next(true);
    this.showPlayersByHand$.next(false);
  }
}