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
    this.svc.queryByStatus(SponsorshipStatus.PendingManagerApproval, { page: 1, size: 20 }).subscribe((r: any) => {
      this.items = r.items || [];
      this.loading = false;
    });
  }

  openApproval(item: any) {
    this.activeItem = item;
    this.approvalDialogVisible = true;
  }

  onApprove($event: any) {
    this.approvalDialogVisible = false;
    // call proxy approve/reject based on $event.approved
    this.load();
  }
}
