import { NavBarModule } from './../nav-bar/nav-bar.module';
import { ReactiveFormsModule } from '@angular/forms';
import { PlayersComponent } from './players.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule, MatInputModule, MatNativeDateModule, MatPaginatorModule, MatProgressSpinnerModule, MatSortModule, MatTableModule, MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material';
import { HttpClientModule } from '@angular/common/http';
import { PlayersBaseModule } from './players-base/players-base.module';

@NgModule({
  declarations: [ PlayersComponent ],
  imports: [
    CommonModule,
    HttpClientModule,
    MatProgressSpinnerModule,
    NavBarModule,
    PlayersBaseModule,
  ],
  providers: [
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'fill' } },
  ],
  exports: [ PlayersComponent ]
})
export class PlayersModule {}
