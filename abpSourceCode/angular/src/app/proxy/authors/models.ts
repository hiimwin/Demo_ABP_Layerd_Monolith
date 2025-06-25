import type { AuditedEntityDto } from '@abp/ng.core';
import type { BookDto } from '../books/models';

export interface AuthorDto extends AuditedEntityDto<string> {
  name?: string;
  dateOfBirth?: string;
  nationality?: string;
  biography?: string;
  avartalUrl?: string;
  books: BookDto[];
}

export interface CreateUpdateAuthorDto {
  name?: string;
  dateOfBirth?: string;
  nationality?: string;
  biography?: string;
  avartalUrl?: string;
}
