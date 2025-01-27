import { NavBarModule } from '../nav-bar/nav-bar.module';
import { ReactiveFormsModule } from '@angular/forms';
import { TournamentsComponent } from './tournaments.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule, MatInputModule, MatNativeDateModule, MatPaginatorModule, MatTableModule, MAT_FORM_FIELD_DEFAULT_OPTIONS, MatSortModule } from '@angular/material';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [ TournamentsComponent ],
  imports: [
    CommonModule,
    MatTableModule,
    MatFormFieldModule,
    MatSortModule,
    MatPaginatorModule,
    MatNativeDateModule,
    ReactiveFormsModule,
    MatInputModule,
    HttpClientModule,
    NavBarModule
  ],
  providers: [
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'fill' } },
  ],
  exports: [ TournamentsComponent ]
})
export class TournamentsModule {}
