import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { CategoryService, CategoryDto } from '../proxy/categories';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category',
  standalone: false,
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss'],
  // Dùng NgbDateNativeAdapter để sử dụng ngày tháng theo định dạng JavaScript Date
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class CategoryComponent implements OnInit {
  category = { items: [], totalCount: 0 } as PagedResultDto<CategoryDto>;
  form: FormGroup;
  selectedCategory = {} as CategoryDto;
  isModalOpen = false; // add this line

  constructor(
    public readonly list: ListService,
    private categoryService: CategoryService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private router: Router
  ) {}

  ngOnInit() {
    const categoryStreamCreator = query => this.categoryService.getList(query);

    this.list.hookToQuery(categoryStreamCreator).subscribe(response => {
      this.category = response;
    });
  }
  buildForm() {
    this.form = this.fb.group({

      name: [
        this.selectedCategory.name || '',
        Validators.required
      ],

      code: [
        this.selectedCategory.code || '',
      ],

      description: [
        this.selectedCategory.description || '',
      ],

      slug: [
        this.selectedCategory.slug || '',
      ],

      imageUrl: [
        this.selectedCategory.imageUrl || '',
      ],

      seoTitle: [
        this.selectedCategory.seoTitle || '',
      ],

      seoDescription: [
        this.selectedCategory.seoDescription || '',
      ],

      displayOrder: [
        this.selectedCategory.displayOrder || 0,
      ],

      isActive: [
        this.selectedCategory.isActive ?? true,
      ],

    });
  }
  // Add editCategory method (form)
  editCategory(id: string) {
    this.categoryService.get(id).subscribe(category => {
      this.selectedCategory = category;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  //Add editCategory method (detail page)
  editCategoryForm(id: string) {
    this.router.navigate(['/categories/edit', id]);
  }


  detailCategory(id: string) {
    // console.log('View Detail category with ID:', id);
    // Muốn router thì cần phải import trong category-routing.module.ts
    this.router.navigate(['/categories', id]);
  }

  // add new method
  createCategory() {
    this.selectedCategory = {} as CategoryDto; // reset the selected category
    this.buildForm();
    this.isModalOpen = true;
  }
  // Add a delete method
  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.categoryService.delete(id).subscribe(() => this.list.get());
      }
    });
  }
  // change the save method
  save() {
    if (this.form.invalid) {
      return;
    }
    // console.log('Form value:', this.form.value);
    const request = this.selectedCategory.id
      ? this.categoryService.update(this.selectedCategory.id, this.form.value)
      : this.categoryService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
}
