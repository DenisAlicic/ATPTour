import { MatTableModule } from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { NgModule } from "@angular/core";
import { PlayersBaseComponent } from "./players-base.component";
import { MatFormFieldModule, MatInputModule, MatNativeDateModule, MatPaginatorModule, MatSortModule, MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [ PlayersBaseComponent ],
  imports: [
    CommonModule,
    MatTableModule,
    MatFormFieldModule,
    MatSortModule,
    MatPaginatorModule,
    MatNativeDateModule,
    ReactiveFormsModule,
    MatInputModule,
  ],
  providers: [
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'fill' } },
  ],
  exports: [ PlayersBaseComponent ]
})
export class PlayersBaseModule {}