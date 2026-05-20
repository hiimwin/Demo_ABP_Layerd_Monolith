import { CoreModule, ListService, PagedResultDto } from "@abp/ng.core";
import { ThemeSharedModule  } from '@abp/ng.theme.shared';
import { Component, OnInit, Input, Output, EventEmitter, SimpleChanges } from "@angular/core";
import { FormGroup } from "@angular/forms";
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
    bookOfCategory = { items: [], totalCount: 0 } as PagedResultDto<BookDto>;
    selectedBook: BookDto = {} as BookDto;
    selectedBooks: BookDto[] = [];
    form!: FormGroup;
    sorts: any[] = [{ prop: 'name', dir: 'asc' }];
    private _clearTrigger!: boolean;
    @Input() visible = false;
    // @Input() categoryId: string | null = null;
    // @Input() initBook: PagedResultDto<BookDto> = { items: [], totalCount: 0 };
    @Input()
        set clearTrigger(value: boolean) {
        if (value !== this._clearTrigger) {
            this._clearTrigger = value;
            this.clearSelection();
        }
    }
    @Output() visibleChange = new EventEmitter<boolean>();
    @Output() selected = new EventEmitter<BookDto[]>();
    @Output() close = new EventEmitter<void>();

    private _categoryId: string | null = null;
    @Input() set categoryId(value: string | null) {
        this._categoryId = value;
        this.filterBooks(); // Gọi lọc lại mỗi khi categoryId có giá trị mới
    }
    get categoryId(): string | null {
        return this._categoryId;
    }
    private _initBook: PagedResultDto<BookDto> = { items: [], totalCount: 0 };
    @Input() set initBook(value: PagedResultDto<BookDto>) {
        this._initBook = value || { items: [], totalCount: 0 };
        this.book = this._initBook;
        this.filterBooks(); // Gọi lọc lại mỗi khi danh sách sách từ cha đổ về
        this.isSelectedByCategory();
    }
    get initBook(): PagedResultDto<BookDto> {
        return this._initBook;
    }

    private filterBooks(): void {
        const filteredBookOfCategory = this.initBook?.items?.filter(s => s.categoryId === this.categoryId) ?? [];
        this.bookOfCategory = {
            items: filteredBookOfCategory,
            totalCount: filteredBookOfCategory.length
        };
    }
    isSelectedByCategory(): void {
        const existsBookByCategory = this.initBook?.items?.filter(s => s.categoryId === this.categoryId) ?? [];
        if (existsBookByCategory) {
            existsBookByCategory.forEach(element => {
                this.selectedBooks.push(element);
            });
        }
    }
    constructor(
        private bookService: BookService,
        private list: ListService,
        private listOriginalBook: ListService
    ) {}
    
    ngOnInit(): void {
        return;
    }

    clearSelection() {
        this.selectedBooks = [];
    }

    removeBook(id: string) {
        this.selectedBooks = this.selectedBooks.filter(s => s.id !== id);
    }

    closeModal() {
        this.visible = false;
        this.visibleChange.emit(false);
        this.close.emit()
    }
    
    handleModalChange(event: boolean) {
        this.visible = event;
        this.visibleChange.emit(event);
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
            this.addBookInListDetail(book);
        } else {
            this.selectedBooks = this.selectedBooks.filter(x => x.id !== book.id);
            this.removeBookInListDetail(book);

        }
    }

    removeBookInListDetail(book: BookDto) {
        const filteredBookOfCategory = this.bookOfCategory?.items?.filter(s => s.id !== book.id) ?? [];
        
        this.bookOfCategory = {
            items: filteredBookOfCategory,
            totalCount: filteredBookOfCategory.length
        };
    }

    addBookInListDetail(book: BookDto) {
        debugger
        const filtered = this.initBook?.items?.filter(s => s.id === book.id) ?? [];
        const current = this.bookOfCategory?.items ?? [];
        const mergedBookOfCategory = [
            ...new Map([...current, ...filtered].map(item => [item.id, item])).values()
        ];

        this.bookOfCategory = {
            items: mergedBookOfCategory,
            totalCount: mergedBookOfCategory.length
        };
    }

    onSort(even: any) {
        this.sorts = [...even.sorts]
    }

    onCustomBookSort(event: any) {
        const sortCriteria = event.sorts && event.sorts[0]; 
        if (!sortCriteria) return;

        if (sortCriteria.prop === 'selected') {
            const direction = sortCriteria.dir;

            if (!this.initBook || !this.initBook.items) return;

            // Tiến hành sort mảng
            const sortedItems = [...this.initBook.items.sort((a, b) => {
            const aChecked = this.isSelected(a?.id) ? 1 : 0;
            const bChecked = this.isSelected(b?.id) ? 1 : 0;

            return direction === 'asc' ? aChecked - bChecked : bChecked - aChecked;
            })];

            this.initBook = {
            ...this.initBook,
            items: sortedItems
            };
        } 
    }
}
