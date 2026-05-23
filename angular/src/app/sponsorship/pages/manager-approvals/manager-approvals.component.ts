import { Component, OnInit } from '@angular/core';
import { SponsorshipService } from '../../services/sponsorship.service';
import { SponsorshipStatus } from '../../enums/status.enum';

@Component({
  selector: 'app-manager-approvals',
  standalone: false,
  templateUrl: './manager-approvals.component.html',
  styleUrls: ['./manager-approvals.component.scss'],
})
export class ManagerApprovalsPageComponent implements OnInit {
  items: any[] = [];
  loading = false;
  approvalDialogVisible = false;
  activeItem: any = null;

  constructor(private svc: SponsorshipService) {}

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.loading = true;
    this.svc.queryByStatus(SponsorshipStatus.PendingManagerApproval).subscribe((r) => {
      this.items = r.items || [];
      this.loading = false;
    });
  }

  openApproval(item: any) {
    this.activeItem = item;
    this.approvalDialogVisible = true;
  }

  onApprove($event: { approved: boolean; remarks?: string }) {
    if (!this.activeItem?.id) return;
    const action = $event.approved
      ? this.svc.approveByManager(this.activeItem.id, $event.remarks)
      : this.svc.rejectByManager(this.activeItem.id, $event.remarks);
    this.approvalDialogVisible = false;
    action.subscribe(() => this.load());
  }
}
