import { Component, OnInit } from '@angular/core';
import { SponsorshipService } from '../../services/sponsorship.service';
import { SponsorshipStatus } from '../../enums/status.enum';

@Component({
  selector: 'app-my-requests',
  standalone: false,
  templateUrl: './my-requests.component.html',
  styleUrls: ['./my-requests.component.scss'],
})
export class MyRequestsPageComponent implements OnInit {
  items: any[] = [];
  loading = false;
  readonly draftStatus = SponsorshipStatus.Draft;

  constructor(private svc: SponsorshipService) {}

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.loading = true;
    this.svc.getMyRequests().subscribe((r) => {
      this.items = r.items || [];
      this.loading = false;
    });
  }

  canCancel(status: SponsorshipStatus) {
    return status === SponsorshipStatus.Draft || status === SponsorshipStatus.PendingManagerApproval;
  }

  onCancel(id: string) {
    if (!confirm('Cancel request?')) return;
    this.svc.cancel(id).subscribe(() => this.load());
  }
}
