import type { AuditedEntityDto } from '@abp/ng.core';
import type { BookDto } from '../books/models';

export interface CreateOrderItemDto {
  quanlity: number;
  unitPrice: number;
  notes?: string;
  bookId?: string;
}

export interface OrderItemDto extends AuditedEntityDto<number> {
  quanlity: number;
  unitPrice: number;
  totalPrice: number;
  notes?: string;
  orderId?: string;
  bookId?: string;
  book: BookDto;
}
