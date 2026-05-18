import type { AuditedEntityDto } from '@abp/ng.core';
import type { BookType } from './book-type.enum';

export interface BookDto extends AuditedEntityDto<string> {
  name?: string;
  type?: BookType;
  publishDate?: string;
  price: number;
  description?: string;
  authorId?: string;
  categoryId?: string;
}

export interface CreateUpdateBookDto {
  name?: string;
  type?: BookType;
  publishDate?: string;
  price: number;
  description?: string;
  authorId?: string;
  categoryId?: string;
}
