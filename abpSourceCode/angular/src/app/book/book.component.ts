import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';

import { BookService, BookDto, bookTypeOptions } from '../proxy/books';

import {
  NgbDateNativeAdapter,
  NgbDateAdapter,
} from '@ng-bootstrap/ng-bootstrap';

import {
  FormGroup,
  FormBuilder,
  Validators,
} from '@angular/forms';

import { Router } from '@angular/router';

import {
  CategoryService,
  CategoryDto,
} from '../proxy/categories';

@Component({
  selector: 'app-book',
  standalone: false,
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss'],

  providers: [
    ListService,
    {
      provide: NgbDateAdapter,
      useClass: NgbDateNativeAdapter,
    },
  ],
})
export class BookComponent implements OnInit {

  book = {
    items: [],
    totalCount: 0,
  } as PagedResultDto<BookDto>;

  categories: CategoryDto[] = [];

  form: FormGroup;

  selectedBook = {} as BookDto;

  isModalOpen = false;

  bookTypes = bookTypeOptions;

  constructor(
    public readonly list: ListService,

    private bookService: BookService,

    private categoryService: CategoryService,

    private fb: FormBuilder,

    private confirmation: ConfirmationService,

    private router: Router
  ) {}

  ngOnInit() {

    // LOAD BOOK LIST
    const bookStreamCreator = query =>
      this.bookService.getList(query);

    this.list
      .hookToQuery(bookStreamCreator)
      .subscribe(response => {
        this.book = response;
      });

    // LOAD CATEGORY DROPDOWN
    this.categoryService
      .getList({
        skipCount: 0,
        maxResultCount: 100,
      })
      .subscribe(response => {
        this.categories = response.items;
      });
  }

  getCategoryName(categoryId: string) {
    return this.categories.find(x => x.id === categoryId)?.name || 'N/A';
  }

  buildForm() {

    this.form = this.fb.group({

      name: [
        this.selectedBook.name || '',
        Validators.required,
      ],

      type: [
        this.selectedBook.type || null,
        Validators.required,
      ],

      publishDate: [
        this.selectedBook.publishDate
          ? new Date(this.selectedBook.publishDate)
          : null,
        Validators.required,
      ],

      price: [
        this.selectedBook.price || null,
        Validators.required,
      ],

      description: [
        this.selectedBook.description || '',
      ],

      // CATEGORY FK
      categoryId: [
        this.selectedBook.categoryId || null,
        // Validators.required,
      ],
    });
  }

  // EDIT
  editBook(id: string) {

    this.bookService
      .get(id)
      .subscribe(book => {

        this.selectedBook = book;

        this.buildForm();

        this.isModalOpen = true;
      });
  }

  // DETAIL
  detailBook(id: string) {

    this.router.navigate(['/books', id]);
  }

  // CREATE
  createBook() {

    this.selectedBook = {} as BookDto;

    this.buildForm();

    this.isModalOpen = true;
  }

  // DELETE
  delete(id: string) {

    this.confirmation
      .warn('::AreYouSureToDelete', '::AreYouSure')
      .subscribe(status => {

        if (status === Confirmation.Status.confirm) {

          this.bookService
            .delete(id)
            .subscribe(() => this.list.get());
        }
      });
  }

  // SAVE
  save() {

    if (this.form.invalid) {
      return;
    }
    const request = this.selectedBook.id
      ? this.bookService.update(
          this.selectedBook.id,
          this.form.value
        )
      : this.bookService.create(
          this.form.value
        );

    request.subscribe(() => {

      this.isModalOpen = false;

      this.form.reset();

      this.list.get();
    });
  }
}