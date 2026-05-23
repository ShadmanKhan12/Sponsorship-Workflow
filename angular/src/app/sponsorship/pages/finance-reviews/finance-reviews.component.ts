import { Component, OnInit } from '@angular/core';
import { SponsorshipService } from '../../services/sponsorship.service';
import { SponsorshipStatus } from '../../enums/status.enum';

@Component({
  selector: 'app-finance-reviews',
  standalone: false,
  templateUrl: './finance-reviews.component.html',
  styleUrls: ['./finance-reviews.component.scss'],
})
export class FinanceReviewsPageComponent implements OnInit {
  items: any[] = [];
  loading = false;
  activeItem: any = null;
  dialogVisible = false;

  constructor(private svc: SponsorshipService) {}

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.loading = true;
    this.svc.queryByStatus(SponsorshipStatus.PendingFinanceReview).subscribe((r) => {
      this.items = r.items || [];
      this.loading = false;
    });
  }

  openReview(it: any) {
    this.activeItem = it;
    this.dialogVisible = true;
  }

  onReviewed($event: { approved: boolean; remarks?: string }) {
    if (!this.activeItem?.id) return;
    const action = $event.approved
      ? this.svc.approveByFinance(this.activeItem.id, $event.remarks)
      : this.svc.rejectByFinance(this.activeItem.id, $event.remarks);
    this.dialogVisible = false;
    action.subscribe(() => this.load());
  }
}
