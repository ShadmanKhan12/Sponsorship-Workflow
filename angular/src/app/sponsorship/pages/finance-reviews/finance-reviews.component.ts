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
    this.svc.queryByStatus(SponsorshipStatus.PendingFinanceReview, { page: 1, size: 20 }).subscribe((r: any) => {
      this.items = r.items || [];
      this.loading = false;
    });
  }

  openReview(it: any) {
    this.activeItem = it;
    this.dialogVisible = true;
  }

  onReviewed($event: any) {
    this.dialogVisible = false;
    this.load();
  }
}
