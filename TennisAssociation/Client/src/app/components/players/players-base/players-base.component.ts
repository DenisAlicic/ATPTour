import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { animate, state, style, transition, trigger } from "@angular/animations";
import { Component, Input, OnInit, ViewChild } from "@angular/core";
import { PlayerModel } from 'src/app/models/players.model';

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
      this.dataSource = new MatTableDataSource(this.players.sort(p => p.currentRankingSingle));
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }
  }

  getCountryCode(country) {
    if (!country) {
      return ''
    }
    if (country == 'USA') {
      return 'flag-icon-us'
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
