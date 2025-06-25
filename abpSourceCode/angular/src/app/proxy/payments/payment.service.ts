import type { CreateUpdatePaymentDto, PaymentDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  apiName = 'Default';
  

  create = (input: CreateUpdatePaymentDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PaymentDto>({
      method: 'POST',
      url: '/api/app/payment',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/payment/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PaymentDto>({
      method: 'GET',
      url: `/api/app/payment/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<PaymentDto>>({
      method: 'GET',
      url: '/api/app/payment',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdatePaymentDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PaymentDto>({
      method: 'PUT',
      url: `/api/app/payment/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
