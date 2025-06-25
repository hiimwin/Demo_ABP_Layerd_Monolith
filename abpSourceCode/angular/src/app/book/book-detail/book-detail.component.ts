import { CoreModule } from '@abp/ng.core';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookDto, BookService, bookTypeOptions } from 'src/app/proxy/books';

@Component({
  selector: 'app-book-detail',
  imports: [CoreModule], // Import
  templateUrl: './book-detail.component.html',
  styleUrl: './book-detail.component.scss',
})
export class BookDetailComponent implements OnInit {
  book: BookDto = {} as BookDto;
  bookTypes = bookTypeOptions;

  constructor(private route: ActivatedRoute, private bookService: BookService) {}

  ngOnInit(): void {
    console.log('BookDetailComponent initialized', this.bookTypes);
    const bookStreamCreator = query => this.bookService.getList(query);

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.bookService.get(id).subscribe(data => {
        this.book = data;
        console.log('Object is:', this.book);
      });
      // console.log('BookDetailComponent initialized with book:', id);
    }
  }

  getBookTypeName(type: number): string {
    const typeOption = this.bookTypes.find(option => option.value === type);
    return typeOption ? typeOption.key : 'Unknown';
  }
}
