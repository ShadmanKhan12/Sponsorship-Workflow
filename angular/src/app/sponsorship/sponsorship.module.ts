import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SponsorshipRoutingModule } from './sponsorship-routing.module';

// Pages
import { DashboardPageComponent } from './pages/dashboard/dashboard.component';
import { MyRequestsPageComponent } from './pages/my-requests/my-requests.component';
import { CreateRequestPageComponent } from './pages/create-request/create-request.component';
import { RequestDetailsPageComponent } from './pages/request-details/request-details.component';
import { ManagerApprovalsPageComponent } from './pages/manager-approvals/manager-approvals.component';
import { FinanceReviewsPageComponent } from './pages/finance-reviews/finance-reviews.component';
import { AdminDashboardPageComponent } from './pages/admin-dashboard/admin-dashboard.component';
import { SponsorshipTypesPageComponent } from './pages/sponsorship-types/sponsorship-types.component';

// Components
import { RequestFormComponent } from './components/request-form/request-form.component';
import { WorkflowTimelineComponent } from './components/workflow-timeline/workflow-timeline.component';
import { StatusBadgeComponent } from './components/status-badge/status-badge.component';
import { ApprovalDialogComponent } from './components/approval-dialog/approval-dialog.component';

@NgModule({
  declarations: [
    DashboardPageComponent,
    MyRequestsPageComponent,
    CreateRequestPageComponent,
    RequestDetailsPageComponent,
    ManagerApprovalsPageComponent,
    FinanceReviewsPageComponent,
    AdminDashboardPageComponent,
    SponsorshipTypesPageComponent,

    RequestFormComponent,
    WorkflowTimelineComponent,
    StatusBadgeComponent,
    ApprovalDialogComponent,
  ],
  imports: [CommonModule, ReactiveFormsModule, FormsModule, SponsorshipRoutingModule],
})
export class SponsorshipModule {}
