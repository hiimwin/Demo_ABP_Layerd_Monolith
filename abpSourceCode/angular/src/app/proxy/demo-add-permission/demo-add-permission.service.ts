import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DemoAddPermissionService {
  apiName = 'Default';
  

  grantRolePermissionDemo = (roleName: string, permission: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/demo-add-permission/grant-role-permission-demo',
      params: { roleName, permission },
    },
    { apiName: this.apiName,...config });
  

  grantUserPermissionDemo = (userId: string, roleName: string, permission: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/demo-add-permission/grant-user-permission-demo/${userId}`,
      params: { roleName, permission },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
