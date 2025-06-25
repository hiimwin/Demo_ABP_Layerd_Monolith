import type { OrderStatus } from './order-status.enum';
import type { CreateOrderItemDto, OrderItemDto } from '../order-items/models';
import type { AuditedEntityDto } from '@abp/ng.core';
import type { PaymentDto } from '../payments/models';

export interface CreateUpdateOrderDto {
  orderDate?: string;
  totalAmout: number;
  address?: string;
  status?: OrderStatus;
  orderItems: CreateOrderItemDto[];
}

export interface OrderDto extends AuditedEntityDto<string> {
  orderDate?: string;
  totalAmout: number;
  address?: string;
  status?: OrderStatus;
  orderItems: OrderItemDto[];
  payment: PaymentDto;
}
