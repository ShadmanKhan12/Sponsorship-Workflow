import { Component, Input } from '@angular/core';
import { SponsorshipStatus } from '../../enums/status.enum';

@Component({
  selector: 'app-status-badge',
  standalone: false,
  templateUrl: './status-badge.component.html',
  styleUrls: ['./status-badge.component.scss'],
})
export class StatusBadgeComponent {
  @Input() status?: SponsorshipStatus | string;

  classes() {
    switch (this.status) {
      case SponsorshipStatus.Draft:
        return 'badge draft';
      case SponsorshipStatus.PendingManagerApproval:
      case SponsorshipStatus.PendingFinanceReview:
        return 'badge pending';
      case SponsorshipStatus.Approved:
        return 'badge approved';
      case SponsorshipStatus.Rejected:
        return 'badge rejected';
      case SponsorshipStatus.Cancelled:
        return 'badge cancelled';
      default:
        return 'badge';
    }
  }
}
