import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
// import { BookService, BookDto, BookType } from '@proxy/books';  // cái nyaf của thằng cũ
import { BookService, BookDto, bookTypeOptions } from '../proxy/books';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, Validators } from '@angular/forms'; // add this
import { Router } from '@angular/router';

@Component({
  selector: 'app-book',
  standalone: false,
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss'],
  // Dùng NgbDateNativeAdapter để sử dụng ngày tháng theo định dạng JavaScript Date
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class BookComponent implements OnInit {
  book = { items: [], totalCount: 0 } as PagedResultDto<BookDto>;
  form: FormGroup;
  selectedBook = {} as BookDto;
  isModalOpen = false; // add this line
  bookTypes = bookTypeOptions;

  constructor(
    public readonly list: ListService,
    private bookService: BookService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private router: Router
  ) {}

  ngOnInit() {
    const bookStreamCreator = query => this.bookService.getList(query);

    this.list.hookToQuery(bookStreamCreator).subscribe(response => {
      this.book = response;
    });
  }
  buildForm() {
    this.form = this.fb.group({
      name: [this.selectedBook.name || '', Validators.required],
      type: [this.selectedBook.type || null, Validators.required],
      publishDate: [
        this.selectedBook.publishDate ? new Date(this.selectedBook.publishDate) : null,
        Validators.required,
      ],
      price: [this.selectedBook.price || null, Validators.required],
      description: [this.selectedBook.description || ''],
    });
  }
  // Add editBook method
  editBook(id: string) {
    // this.bookService.get(id).subscribe(book => {
    //   this.selectedBook = book;
    //   this.buildForm();
    //   this.isModalOpen = true;
    // });

    this.bookService.get(id).subscribe(book => {
      this.selectedBook = book;
      this.buildForm();
      this.isModalOpen = true;
    });
  }
  detailBook(id: string) {
    // console.log('View Detail book with ID:', id);
    // Muốn router thì cần phải import trong book-routing.module.ts
    this.router.navigate(['/books', id]);
  }

  // add new method
  createBook() {
    this.selectedBook = {} as BookDto; // reset the selected book
    this.buildForm();
    this.isModalOpen = true;
  }
  // Add a delete method
  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.bookService.delete(id).subscribe(() => this.list.get());
      }
    });
  }
  // change the save method
  save() {
    if (this.form.invalid) {
      return;
    }
    // console.log('Form value:', this.form.value);
    const request = this.selectedBook.id
      ? this.bookService.update(this.selectedBook.id, this.form.value)
      : this.bookService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
}
