import type { AuditedEntityDto } from '@abp/ng.core';
import type { BookDto } from '../books/models';

export interface CategoryDto extends AuditedEntityDto<string> {
  name?: string;
  code?: string;
  description?: string;
  slug?: string;
  imageUrl?: string;
  seoTitle?: string;
  seoDescription?: string;
  displayOrder?: number;
  isActive?: boolean;
  books: BookDto[];
}

export interface CreateUpdateCategoryDto extends AuditedEntityDto<string> {
  name?: string;
  code?: string;
  description?: string;
  slug?: string;
  imageUrl?: string;
  seoTitle?: string;
  seoDescription?: string;
  displayOrder?: number;
  isActive?: boolean;
}
