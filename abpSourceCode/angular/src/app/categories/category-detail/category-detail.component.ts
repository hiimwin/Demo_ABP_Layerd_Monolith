import { CoreModule } from '@abp/ng.core';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryDto, CategoryService } from 'src/app/proxy/categories';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

@Component({
  selector: 'app-category-detail',
  imports: [CoreModule, NgxDatatableModule], // Import cái này vào để sử dụng abpLocalization trong html
  templateUrl: './category-detail.component.html',
  styleUrl: './category-detail.component.scss',
})
export class CategoryDetailComponent implements OnInit {
  category: CategoryDto = {} as CategoryDto;

  constructor(private route: ActivatedRoute, private categoryService: CategoryService) {}

  ngOnInit(): void {
    const categoryStreamCreator = query => this.categoryService.getList(query);

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.categoryService.get(id).subscribe(data => {
        this.category = data;
        console.log('Object is:', this.category);
      });
      // console.log('categoryDetailComponent initialized with category:', id);
    }
  }
}
