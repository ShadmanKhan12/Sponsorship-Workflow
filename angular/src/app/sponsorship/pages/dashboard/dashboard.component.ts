import { Component, OnInit } from '@angular/core';
import { SponsorshipService } from '../../services/sponsorship.service';
import { SponsorshipStatus } from '../../enums/status.enum';

@Component({
  selector: 'app-sponsorship-dashboard',
  standalone: false,
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardPageComponent implements OnInit {
  summary: any = { total: 0, pending: 0, approved: 0, rejected: 0 };

  constructor(private svc: SponsorshipService) {}

  ngOnInit(): void {
    this.loadSummary();
  }

  loadSummary() {
    // Using proxy endpoints for counts would be ideal; here we fetch lists for counts as a placeholder
    this.svc.queryByStatus(SponsorshipStatus.Draft).subscribe((r: any) => (this.summary.total += r.totalCount || 0));
    this.svc.queryByStatus(SponsorshipStatus.PendingManagerApproval).subscribe((r: any) => (this.summary.pending = r.totalCount || 0));
    this.svc.queryByStatus(SponsorshipStatus.Approved).subscribe((r: any) => (this.summary.approved = r.totalCount || 0));
    this.svc.queryByStatus(SponsorshipStatus.Rejected).subscribe((r: any) => (this.summary.rejected = r.totalCount || 0));
  }
}
