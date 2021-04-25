import { TitleCasePipe, UpperCasePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Color } from 'ng2-charts';
import { BehaviorSubject } from 'rxjs';
import { StatisticsService } from 'src/app/services/statistics.service';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.scss']
})
export class StatisticsComponent implements OnInit {
  // Pie
  handPieChartLabels$ = new BehaviorSubject([]);
  handPieChartData$ = new BehaviorSubject([]);

  handPieOptions: any = {
    legend: { position: 'left' }
  }

  pieChartColors: Array<any> = [{
    backgroundColor: ['#ac97c1', '#71839d'],
  }];

  constructor(
    private statisticsService: StatisticsService,
    private titleCasePipe: TitleCasePipe
  ) {}

  // events
  // public chartClicked(e:any):void {
  //   console.log(e);
  // }
  
  // public chartHovered(e:any):void {
  //   console.log(e);
  // }

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
}