import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms'; // add this

@Component({
  selector: 'app-category-table',
  standalone: false,
  templateUrl: './category-table.component.html',
  styleUrl: './category-table.component.scss',
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class CategoryTableComponent implements OnInit {
  form: FormGroup;

  constructor(public readonly list: ListService, private fb: FormBuilder) {}
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  @Input() categories: any[] = [];

  // @Output() onEdit = new EventEmitter<number>();
  // @Output() onDelete = new EventEmitter<number>();
  // @Output() onDetail = new EventEmitter<number>();
}
