import { CoreModule, ListService, PagedResultDto  } from '@abp/ng.core';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryDto, CategoryService } from 'src/app/proxy/categories';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToasterService, ConfirmationService, Confirmation, ThemeSharedModule } from '@abp/ng.theme.shared';
import {
  NgbDateNativeAdapter,
  NgbDateAdapter,
} from '@ng-bootstrap/ng-bootstrap';
import { BookDto, BookService } from 'src/app/proxy/books';
import { CategoryBookSelectorComponent } from '../category-book-selector/category-book-selector.component';

@Component({
  selector: 'app-category-form',
  standalone: true,
  imports: [
    CommonModule,
    CoreModule,
    ThemeSharedModule,
    CategoryBookSelectorComponent
  ],
  providers: [
      ListService,
      {
        provide: NgbDateAdapter,
        useClass: NgbDateNativeAdapter,
      },
    ],
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.scss'],
})
export class CategoryFormComponent implements OnInit {
  category = { items: [], totalCount: 0 } as PagedResultDto<CategoryDto>;
  book = { items: [], totalCount: 0 } as PagedResultDto<BookDto>;
  selectedBook: BookDto = {} as BookDto;
  selectedBooks: BookDto[] = [];
  selectedCategory = {} as CategoryDto;
  form!: FormGroup;
  isEdit = false;
  isOpenBook = false;

  constructor(
    private route: ActivatedRoute,
    private categoryService: CategoryService,
    private bookService: BookService,
    private fb: FormBuilder,
    private router: Router,
    private toaster: ToasterService,
    private list: ListService
  ) {}

  ngOnInit(): void {
    const categoryStreamCreator = query => this.categoryService.getList(query);
    this.list.hookToQuery(categoryStreamCreator).subscribe(response => {
      this.category = response;
    });
    
    const bookStreamCreator = query => this.bookService.getList(query);
      this.list.hookToQuery(bookStreamCreator).subscribe(response => {
      const filteredItems = response?.items?.filter(s => s.categoryId === null) ?? [];
      this.book = {
        items: filteredItems,
        totalCount: filteredItems.length // Hoặc response?.totalCount nếu muốn giữ tổng gốc
      };
    });

    const id = this.route.snapshot.paramMap.get('id');
    this.buildForm();
    if (id) {
      this.isEdit = true;
      this.categoryService.get(id).subscribe(data => {
        this.selectedCategory = data;
        this.form.patchValue(data); 
      });
    } 
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

  back() {
    this.router.navigate(['/categories']);
  }
  

  save() {
    if (this.form.invalid) {
      return;
    }
    const id = this.selectedCategory?.id;
    const request = {
      ...this.form.value,
      books: this.selectedBooks.map(x => x.id)
    };
    debugger
    this.categoryService.update(id, request).subscribe({
      next: () => {
        this.toaster.success('Updated successfully');
      },
      error: () => {
        this.toaster.error('Update failed');
      }
    });
  }

  addNew() {
    if (this.form.invalid) {
      return;
    }
    this.categoryService.create(this.form.value).subscribe({
        next: () => {
            this.toaster.success('Created successfully');
            this.form.reset();
            this.back();
        },
        error: () => {
        this.toaster.error('Create failed');
        }
    });
  }

  openBookSelector() {
    this.isOpenBook = true;
  }

  handleBooksSelected(books: BookDto[]) {
    books.forEach(b => {
      if (!this.selectedBooks.find(x => x.id === b.id)) {
        this.selectedBooks.push(b);
      }
    });
    console.log(this.selectedBooks)
  }
}

