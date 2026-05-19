import { CoreModule, ListService, PagedResultDto } from "@abp/ng.core";
import { ToasterService, ConfirmationService, Confirmation, ThemeSharedModule  } from '@abp/ng.theme.shared';
import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { BookDto, BookService } from "src/app/proxy/books";
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-category-book-selector',
    standalone: true,
    imports: [ThemeSharedModule, CoreModule, NgxDatatableModule],
    templateUrl:'./category-book-selector.component.html',
    styleUrl: './category-book-selector.component.scss',
    providers: [
        ListService,
        {
          provide: NgbDateAdapter,
          useClass: NgbDateNativeAdapter,
        },
      ],
})


export class CategoryBookSelectorComponent implements OnInit {
    book = { items: [], totalCount: 0 } as PagedResultDto<BookDto>;
    selectedBook: BookDto = {} as BookDto;
    selectedBooks: BookDto[] = [];
    form!: FormGroup;
    
    @Input() visible = false;
    @Output() selected = new EventEmitter<BookDto[]>();
    @Output() close = new EventEmitter<void>();
    @Input() books: BookDto[] = [];

    constructor(
        private route: ActivatedRoute, 
        private bookService: BookService,
        private fb: FormBuilder,
        private list: ListService,
        private router: Router,
        private toaster: ToasterService,
        private confirmation: ConfirmationService,
    ) {}
    
    ngOnInit(): void {
        const bookStreamCreator = query => this.bookService.getList(query);
        this.list.hookToQuery(bookStreamCreator).subscribe(response => {
            this.book = response;
        });
    }

    removeBook(id: string) {
        this.selectedBooks = this.selectedBooks.filter(s => s.id !== id);
    }

    closeModal() {
        this.close.emit()
    }

    confirm() {
        this.selected.emit(this.selectedBooks);
        this.close.emit();
    }
    
    isSelected(id: string) {
        return this.selectedBooks.some(x => x.id === id);
    }

    onSelect(event: Event, book: BookDto) {
        const input = event.target as HTMLInputElement;
        if (input.checked) {
            const exists = this.selectedBooks.find(x => x.id === book.id);
            if (!exists) {
            this.selectedBooks.push(book);
            }
        } else {
            this.selectedBooks = this.selectedBooks.filter(x => x.id !== book.id);
        }
    }
}