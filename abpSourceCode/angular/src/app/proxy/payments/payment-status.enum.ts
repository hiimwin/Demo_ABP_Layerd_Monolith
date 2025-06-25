import { mapEnumToOptions } from '@abp/ng.core';

export enum PaymentStatus {
  Pending = 0,
  Paid = 1,
  Failed = 2,
  Cancelled = 3,
  Refunded = 4,
}

export const paymentStatusOptions = mapEnumToOptions(PaymentStatus);
