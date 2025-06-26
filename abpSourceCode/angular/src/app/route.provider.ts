import { RoutesService, eLayoutType } from '@abp/ng.core';
import { inject, provideAppInitializer } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  provideAppInitializer(() => {
    configureRoutes();
  }),
];

function configureRoutes() {
  const routes = inject(RoutesService);
  routes.add([
    {
      path: '/',
      name: '::Menu:Home',
      iconClass: 'fas fa-home',
      order: 1,
      layout: eLayoutType.application,
    },
    // Hiển thị trên thanh menu || Nếu import đoạn này vào rồi thì sẽ có thể sài được abpLocalization trong html
    // {
    //   path: '/book-store',
    //   name: '::Menu:BookStore',
    //   iconClass: 'fas fa-book',
    //   order: 2,
    //   layout: eLayoutType.application,
    // },
    // {
    //   path: '/books',
    //   name: '::Menu:Books',
    //   parentName: '::Menu:BookStore',
    //   layout: eLayoutType.application,
    // },
    {
      path: '/books',
      name: '::Menu:Books',
      iconClass: 'fas fa-book',
      layout: eLayoutType.application,
      requiredPolicy: 'abpSourceCode.Books', // Chỉ hiển thị khi có quyền này
    },
  ]);
}
