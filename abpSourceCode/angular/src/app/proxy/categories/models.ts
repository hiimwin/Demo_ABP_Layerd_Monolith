import type { AuditedEntityDto } from '@abp/ng.core';
import type { BookDto } from '../books/models';

export interface CategoryDto extends AuditedEntityDto<string> {
  name?: string;
  description?: string;
  books: BookDto[];
}

export interface CreateUpdateCategoryDto extends AuditedEntityDto<string> {
  name?: string;
  description?: string;
}
