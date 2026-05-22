import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PermissionGuard } from '@abp/ng.core';

import { DashboardPageComponent } from './pages/dashboard/dashboard.component';
import { MyRequestsPageComponent } from './pages/my-requests/my-requests.component';
import { CreateRequestPageComponent } from './pages/create-request/create-request.component';
import { RequestDetailsPageComponent } from './pages/request-details/request-details.component';
import { ManagerApprovalsPageComponent } from './pages/manager-approvals/manager-approvals.component';
import { FinanceReviewsPageComponent } from './pages/finance-reviews/finance-reviews.component';
import { AdminDashboardPageComponent } from './pages/admin-dashboard/admin-dashboard.component';
import { SponsorshipTypesPageComponent } from './pages/sponsorship-types/sponsorship-types.component';

const routes: Routes = [
  {
    path: '',
    children: [
      { path: '', component: DashboardPageComponent },
      { path: 'dashboard', component: DashboardPageComponent },
      { path: 'my-requests', component: MyRequestsPageComponent, canActivate: [PermissionGuard], data: { requiredPolicy: 'SponsorshipWorkflow.SponsorshipRequests.Create' } },
      { path: 'create', component: CreateRequestPageComponent, canActivate: [PermissionGuard], data: { requiredPolicy: 'SponsorshipWorkflow.SponsorshipRequests.Create' } },
      { path: 'details/:id', component: RequestDetailsPageComponent },
      { path: 'manager-approvals', component: ManagerApprovalsPageComponent, canActivate: [PermissionGuard], data: { requiredPolicy: 'SponsorshipWorkflow.SponsorshipRequests.ManagerApprove' } },
      { path: 'finance-reviews', component: FinanceReviewsPageComponent, canActivate: [PermissionGuard], data: { requiredPolicy: 'SponsorshipWorkflow.SponsorshipRequests.FinanceApprove' } },
      { path: 'admin', component: AdminDashboardPageComponent, canActivate: [PermissionGuard], data: { requiredPolicy: 'SponsorshipWorkflow.SponsorshipRequests.ViewAll' } },
      { path: 'types', component: SponsorshipTypesPageComponent, canActivate: [PermissionGuard], data: { requiredPolicy: 'SponsorshipWorkflow.SponsorshipTypes.Manage' } },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SponsorshipRoutingModule {}
