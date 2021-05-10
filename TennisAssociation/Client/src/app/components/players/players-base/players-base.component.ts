import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { animate, state, style, transition, trigger } from "@angular/animations";
import { Component, Input, OnInit, ViewChild } from "@angular/core";
import { PlayerModel } from 'src/app/models/players.model';

export const US = 'USA';
export const GB = 'Great Britain';
export const RSA = 'RSA';
export const MD = 'Moldavsko';
export const BA = 'Bosnia and Herzeg.';
export const CN = 'Taipei (CHN)';
export const DO = 'Dominican Rep.';
export const TN = 'Tunis';
export const MP = 'N. Mariana Isl.';
export const BS = 'Bahamas';

@Component({
  selector: 'app-players-base',
  templateUrl: './players-base.component.html',
  styleUrls: ['./players-base.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class PlayersBaseComponent implements OnInit {
  @Input() players: PlayerModel[] = [];

  displayedColumns: string[] = ['currentRankingSingle', 'name',  'country'];
  dataSource: MatTableDataSource<PlayerModel>;
  expandedElement: PlayerModel | null;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  ngOnInit() {
    if(this.players.length !== 0) {
      this.dataSource = new MatTableDataSource(this.players.sort((player1, player2) => player1.currentRankingSingle - player2.currentRankingSingle));
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }
  }

  getCountryCode(country) {
    switch(country) {
      case '':
        return '';
      case US: 
        return 'flag-icon-us';
      case GB:
        return 'flag-icon-gb';
      case RSA: 
        return 'flag-icon-za';
      case MD:
        return 'flag-icon-md';
      case BA:
        return 'flag-icon-ba';
      case CN:
        return 'flag-icon-cn';
      case DO:
        return 'flag-icon-do';
      case TN:
        return 'flag-icon-tn';
      case MP:
        return 'flag-icon-mp';
      case BS:
        return 'flag-icon-bs';  
    }

    const lookup = require('country-code-lookup')
    const countryInfo = lookup.byCountry(country)
    if (!countryInfo) {
      return ''
    }

    return 'flag-icon-'+countryInfo['iso2'].toLowerCase()
  }

  applyFilter(event: Event) {
    this.dataSource.filterPredicate = function(data, filter: string): boolean {
      return data.firstName.toLowerCase().includes(filter) || data.lastName.toLowerCase().includes(filter);
    };

    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}
