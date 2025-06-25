import { mapEnumToOptions } from '@abp/ng.core';

export enum PaymentMethod {
  Cash = 0,
  CreditCard = 1,
  MoMo = 2,
  VnPay = 3,
}

export const paymentMethodOptions = mapEnumToOptions(PaymentMethod);
