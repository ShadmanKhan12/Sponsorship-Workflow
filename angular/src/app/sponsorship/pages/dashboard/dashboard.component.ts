import { Component, OnInit } from '@angular/core';
import { PermissionService } from '@abp/ng.core';
import { SponsorshipService } from '../../services/sponsorship.service';
import { SponsorshipStatus } from '../../enums/status.enum';
import { SponsorshipRequest } from '../../models/request.model';
import { SponsorshipPermissions } from '../../constants/permissions';

type DashboardMode = 'admin' | 'requestor' | 'manager' | 'finance';

@Component({
  selector: 'app-sponsorship-dashboard',
  standalone: false,
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardPageComponent implements OnInit {
  mode: DashboardMode = 'requestor';
  loading = true;

  summary = {
    total: 0,
    draft: 0,
    pendingManager: 0,
    pendingFinance: 0,
    approved: 0,
    rejected: 0,
  };

  constructor(
    private svc: SponsorshipService,
    private permission: PermissionService
  ) {}

  ngOnInit(): void {
    if (this.permission.getGrantedPolicy(SponsorshipPermissions.requests.viewAll)) {
      this.mode = 'admin';
      this.loadAdminSummary();
    } else if (this.permission.getGrantedPolicy(SponsorshipPermissions.requests.managerApprove)) {
      this.mode = 'manager';
      this.loadManagerSummary();
    } else if (this.permission.getGrantedPolicy(SponsorshipPermissions.requests.financeApprove)) {
      this.mode = 'finance';
      this.loadFinanceSummary();
    } else {
      this.mode = 'requestor';
      this.loadRequestorSummary();
    }
  }

  private loadAdminSummary(): void {
    this.svc.getAllRequests(0, 500).subscribe({
      next: (res) => {
        this.applyCounts(res.items);
        this.loading = false;
      },
      error: () => (this.loading = false),
    });
  }

  private loadRequestorSummary(): void {
    this.svc.getMyRequests().subscribe({
      next: (res) => {
        this.applyCounts(res.items);
        this.loading = false;
      },
      error: () => (this.loading = false),
    });
  }

  private loadManagerSummary(): void {
    this.svc.queryByStatus(SponsorshipStatus.PendingManagerApproval).subscribe({
      next: (res) => {
        this.summary.pendingManager = res.totalCount;
        this.loading = false;
      },
      error: () => (this.loading = false),
    });
  }

  private loadFinanceSummary(): void {
    this.svc.queryByStatus(SponsorshipStatus.PendingFinanceReview).subscribe({
      next: (res) => {
        this.summary.pendingFinance = res.totalCount;
        this.loading = false;
      },
      error: () => (this.loading = false),
    });
  }

  private applyCounts(items: SponsorshipRequest[]): void {
    this.summary.total = items.length;
    this.summary.draft = items.filter((i) => i.status === SponsorshipStatus.Draft).length;
    this.summary.pendingManager = items.filter((i) => i.status === SponsorshipStatus.PendingManagerApproval).length;
    this.summary.pendingFinance = items.filter((i) => i.status === SponsorshipStatus.PendingFinanceReview).length;
    this.summary.approved = items.filter((i) => i.status === SponsorshipStatus.Approved).length;
    this.summary.rejected = items.filter((i) => i.status === SponsorshipStatus.Rejected).length;
  }
}
