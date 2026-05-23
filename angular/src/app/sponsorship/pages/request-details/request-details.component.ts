import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PermissionService } from '@abp/ng.core';
import { SponsorshipService } from '../../services/sponsorship.service';
import { SponsorshipStatus } from '../../enums/status.enum';
import { SponsorshipPermissions } from '../../constants/permissions';

@Component({
  selector: 'app-request-details',
  standalone: false,
  templateUrl: './request-details.component.html',
  styleUrls: ['./request-details.component.scss'],
})
export class RequestDetailsPageComponent implements OnInit {
  id?: string | null;
  item: any = null;
  timeline: any[] = [];
  approvalDialogVisible = false;

  constructor(
    private route: ActivatedRoute,
    private svc: SponsorshipService,
    private permission: PermissionService
  ) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id) this.load();
  }

  readonly status = SponsorshipStatus;

  load() {
    if (!this.id) return;
    this.svc.getById(this.id).subscribe((r) => (this.item = r));
    this.svc.getWorkflowHistory(this.id).subscribe((h) => {
      this.timeline = (h || []).map((x: any) => ({
        timestamp: x.performedAt,
        action: x.action,
        user: x.performedByUserName || 'System',
        previousStatus: x.previousStatus,
        newStatus: x.newStatus,
        remarks: x.remarks,
      }));
    });
  }

  /** Matches workflow: manager step then finance step (backend unchanged). */
  get canApprove(): boolean {
    if (!this.item) return false;
    if (this.item.status === SponsorshipStatus.PendingManagerApproval) {
      return this.permission.getGrantedPolicy(SponsorshipPermissions.requests.managerApprove);
    }
    if (this.item.status === SponsorshipStatus.PendingFinanceReview) {
      return this.permission.getGrantedPolicy(SponsorshipPermissions.requests.financeApprove);
    }
    return false;
  }

  showApproval() {
    this.approvalDialogVisible = true;
  }

  onApproval($event: { approved: boolean; remarks?: string }) {
    if (!this.id || !this.item) return;
    let action;
    if (this.item.status === SponsorshipStatus.PendingManagerApproval) {
      action = $event.approved
        ? this.svc.approveByManager(this.id, $event.remarks)
        : this.svc.rejectByManager(this.id, $event.remarks);
    } else {
      action = $event.approved
        ? this.svc.approveByFinance(this.id, $event.remarks)
        : this.svc.rejectByFinance(this.id, $event.remarks);
    }
    this.approvalDialogVisible = false;
    action.subscribe(() => this.load());
  }
}
