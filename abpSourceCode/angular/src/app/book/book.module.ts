import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BookRoutingModule } from './book-routing.module';
import { BookComponent } from './book.component';
import { SharedModule } from '../shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { BookTableComponent } from './components/book-table/book-table.component';
// NgbDateAdapter
@NgModule({
  declarations: [BookComponent, BookTableComponent],
  imports: [CommonModule, BookRoutingModule, SharedModule, NgbDatepickerModule],
})
export class BookModule {}
