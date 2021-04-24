import { TournamentModel } from '../../models/tournaments.model';
import { TournamentsService } from '../../services/tournaments.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

import {
  trigger,
  state,
  style,
  animate,
  transition,
  // ...
} from '@angular/animations';
@Component({
  selector: 'app-tournaments',
  templateUrl: './tournaments.component.html',
  styleUrls: ['./tournaments.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class TournamentsComponent implements OnInit {
  displayedColumns: string[] = ['name'];
  dataSource: MatTableDataSource<TournamentModel>;
  expandedElement: TournamentModel | null;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(private tournamentsService: TournamentsService) {}

  ngOnInit() {
    this.loadTournaments();
  }

  loadTournaments() {
    this.tournamentsService.getTournaments().subscribe(value => {
      this.dataSource = new MatTableDataSource(value);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  isInfoAvailable(tournament: TournamentModel) {
    return tournament._1RoundPrize || 
      tournament._1RoundPoints || 
      tournament._2RoundPrize || 
      tournament._2RoundPoints || 
      tournament._3RoundPrize || 
      tournament._3RoundPoints || 
      tournament.roundOf16Prize || 
      tournament.roundOf16Points || 
      tournament.quarterfinalPrize || 
      tournament.quarterfinalPoints || 
      tournament.semifinalPrize || 
      tournament.semifinalPoints || 
      tournament.finalPrize || 
      tournament.finalPoints || 
      tournament.winnerPrize || 
      tournament.winnerPoints ? true : false;
  }
}
