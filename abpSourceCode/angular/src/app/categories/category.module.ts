import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryRoutingModule } from './category-routing.module';
import { CategoryComponent } from './category.component';
import { SharedModule } from '../shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { CategoryTableComponent } from './components/category-table/category-table.component';
// NgbDateAdapter
@NgModule({
  declarations: [CategoryComponent, CategoryTableComponent],
  imports: [CommonModule, CategoryRoutingModule, SharedModule, NgbDatepickerModule],
})
export class CategoryModule {}
