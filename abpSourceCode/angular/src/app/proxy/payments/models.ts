import type { PaymentMethod } from './payment-method.enum';
import type { PaymentStatus } from './payment-status.enum';
import type { AuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdatePaymentDto {
  amount?: number;
  method?: PaymentMethod;
  dateTime?: string;
  status?: PaymentStatus;
  orderId?: string;
}

export interface PaymentDto extends AuditedEntityDto<string> {
  amount?: number;
  method?: PaymentMethod;
  dateTime?: string;
  status?: PaymentStatus;
  orderId?: string;
}
