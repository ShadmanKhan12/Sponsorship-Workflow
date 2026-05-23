import { provideAppInitializer, inject } from '@angular/core';
import { RoutesService, eLayoutType } from '@abp/ng.core';
import { SponsorshipPermissions } from './constants/permissions';

export const SPONSORSHIP_MENU_PROVIDER = [
  provideAppInitializer(() => {
    const configure = () => {
      const routes = inject(RoutesService);
      // Parent sponsorship menu
      routes.add([
        {
          path: '/sponsorship',
          name: 'Sponsorship::Menu',
          iconClass: 'fas fa-hands-helping',
          order: 5,
          layout: eLayoutType.application,
          requiredPolicy: SponsorshipPermissions.requests.default,
        },
      ]);

      // Child entries that reference the parent by using parentName
      routes.add([
        { path: '/sponsorship/dashboard', name: 'Sponsorship::Dashboard', iconClass: 'fas fa-chart-pie', order: 1, layout: eLayoutType.application, parentName: 'Sponsorship::Menu', requiredPolicy: SponsorshipPermissions.requests.default },
        { path: '/sponsorship/my-requests', name: 'Sponsorship::MyRequests', iconClass: 'fas fa-list', order: 2, layout: eLayoutType.application, parentName: 'Sponsorship::Menu', requiredPolicy: SponsorshipPermissions.requests.create },
        { path: '/sponsorship/create', name: 'Sponsorship::CreateRequest', iconClass: 'fas fa-plus', order: 3, layout: eLayoutType.application, parentName: 'Sponsorship::Menu', requiredPolicy: SponsorshipPermissions.requests.create },
        { path: '/sponsorship/manager-approvals', name: 'Sponsorship::PendingApprovals', iconClass: 'fas fa-check-double', order: 4, layout: eLayoutType.application, parentName: 'Sponsorship::Menu', requiredPolicy: SponsorshipPermissions.requests.managerApprove },
        { path: '/sponsorship/finance-reviews', name: 'Sponsorship::FinanceReviews', iconClass: 'fas fa-file-invoice-dollar', order: 5, layout: eLayoutType.application, parentName: 'Sponsorship::Menu', requiredPolicy: SponsorshipPermissions.requests.financeApprove },
        { path: '/sponsorship/admin', name: 'Sponsorship::AllRequests', iconClass: 'fas fa-folder-open', order: 6, layout: eLayoutType.application, parentName: 'Sponsorship::Menu', requiredPolicy: SponsorshipPermissions.requests.viewAll },
        { path: '/sponsorship/types', name: 'Sponsorship::Types', iconClass: 'fas fa-tags', order: 7, layout: eLayoutType.application, parentName: 'Sponsorship::Menu', requiredPolicy: SponsorshipPermissions.types.manage },
      ]);
    };

    configure();
  }),
];
