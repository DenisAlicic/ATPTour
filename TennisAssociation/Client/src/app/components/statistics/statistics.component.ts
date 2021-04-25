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
  };
  handPieChartColors: Array<any> = [{
    backgroundColor: ['#ac97c1', '#71839d'],
  }];

  // Height bars
  heightsBarLabels$ = new BehaviorSubject([]);
  heightsBarData$ = new BehaviorSubject([]);
  heightsBarOpions: any = {
    legend: { position: 'left' }
  };

  // Years bars
  yearsBarLabels$ = new BehaviorSubject([]);
  yearsBarData$ = new BehaviorSubject([]);
  yearsBarOpions: any = {
    legend: { position: 'left' }
  };

  showStatistics$ = new BehaviorSubject(true);
  arePlayersLoading$ = new BehaviorSubject(false);
  players$ = new BehaviorSubject([]);
  playersTitle$ = new BehaviorSubject('');

  showPlayersByHand$ = new BehaviorSubject(false);
  showPlayersByHeights$ = new BehaviorSubject(false);
  showPlayersByYears$ = new BehaviorSubject(false);

  constructor(
    private statisticsService: StatisticsService,
    private playersService: PlayersService,
    private titleCasePipe: TitleCasePipe,
    private lowerCasePipe: LowerCasePipe,
  ) {}

  ngOnInit() {
    this.loadHandStatistics();
    this.loadHeightsStatistics();
    this.loadYearsStatistics();
  }

  loadHandStatistics() {
    this.statisticsService.getHandStatistics().subscribe(value => {
      this.handPieChartLabels$.next([this.getLabelFromHand(value[0].hand), this.getLabelFromHand(value[1].hand)]);
      this.handPieChartData$.next([value[0].count, value[1].count]);
    });
  }

  loadHeightsStatistics() {
    this.statisticsService.getHeightsStatistics().subscribe(heights => {
      heights.forEach(el => {
        // Add label
        const labels = this.heightsBarLabels$.getValue();
        labels.push(this.getLabelFromHeight(el.heightRange));
        this.heightsBarLabels$.next(labels);

        // Add value
        const values = this.heightsBarData$.getValue();
        values.push(el.count);
        this.heightsBarData$.next(values);
      });
    });
  }

  loadYearsStatistics() {
    this.statisticsService.getYearsStatistics().subscribe(years => {
      years.forEach(el => {
        // Add label
        const labels = this.yearsBarLabels$.getValue();
        labels.push(this.getLabelFromYears(el.age));
        this.yearsBarLabels$.next(labels);

        // Add value
        const values = this.yearsBarData$.getValue();
        values.push(el.count);
        this.yearsBarData$.next(values);
      });
    });
  }

  getLabelFromActiveChart(chart: any) {
    return chart.active[0] !== undefined ? chart.active[0]._model.label : null;
  }

  getLabelFromHand(hand: string) {
    return this.titleCasePipe.transform(hand + ' Handed'); 
  }

  getHandFromLabel(chart: any) {
    return this.lowerCasePipe.transform(this.getLabelFromActiveChart(chart).split(' ')[0]);
  }

  getLabelFromHeight(heightRange: string) {
    return heightRange.split('-')[0].trim() + ' cm';
  }

  getHeightFromLabel(bar: any) {
    return Number(this.getLabelFromActiveChart(bar).split(' ')[0]);
  }

  getLabelFromYears(age: number) {
    return age + ' years';
  }

  getYearsFromLabel(bar: any) {
    return Number(this.getLabelFromActiveChart(bar).split(' ')[0]);
  }

  handChartClicked(event: any) {
    if(this.getLabelFromActiveChart(event) === null) return;

    this.showStatistics$.next(false);
    this.arePlayersLoading$.next(true);
    this.playersTitle$.next(this.getLabelFromActiveChart(event) + ' Players');
    const hand = this.getHandFromLabel(event);
    this.playersService.getPlayers().subscribe(value => {
      this.players$.next(value.filter(player => player.hand === hand));
      this.arePlayersLoading$.next(false);
      this.showPlayersByHand$.next(true);
    });
  }

  heightBarClicked(event: any) {
    if(this.getLabelFromActiveChart(event) === null) return;

    this.showStatistics$.next(false);
    this.arePlayersLoading$.next(true);
    this.playersTitle$.next('Players with ' + this.getLabelFromActiveChart(event) + ' of height');
    const height = this.getHeightFromLabel(event);
    this.playersService.getPlayers().subscribe(value => {
      this.players$.next(value.filter(player => player.height === height));
      this.arePlayersLoading$.next(false);
      this.showPlayersByHeights$.next(true);
    });
  }

  yearBarClicked(event: any) {
    if(this.getLabelFromActiveChart(event) === null) return;

    this.showStatistics$.next(false);
    this.arePlayersLoading$.next(true);
    this.playersTitle$.next(this.getLabelFromActiveChart(event) + ' old players');
    const years = this.getHeightFromLabel(event);
    this.playersService.getPlayers().subscribe(value => {
      this.players$.next(value.filter(player => (new Date().getFullYear() - new Date(player.birth).getFullYear()) === years));
      this.arePlayersLoading$.next(false);
      this.showPlayersByYears$.next(true);
    });
  }

  backToStatistics() {
    this.showStatistics$.next(true);
    this.arePlayersLoading$.next(false);
    this.showPlayersByHand$.next(false);
    this.showPlayersByHeights$.next(false);
    this.showPlayersByYears$.next(false);
  }
}