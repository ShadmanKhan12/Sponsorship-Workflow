import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SponsorshipService } from '../../services/sponsorship.service';
import { SponsorshipStatus } from '../../enums/status.enum';

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

  constructor(private route: ActivatedRoute, private svc: SponsorshipService) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id) this.load();
  }

  load() {
    if (!this.id) return;
    this.svc.getById(this.id).subscribe((r) => {
      this.item = r;
      // timeline should be fetched from proxy; using placeholder
      this.timeline = (r as any)?.workflowHistory || [];
    });
  }

  showApproval() { this.approvalDialogVisible = true; }

  onApproval($event: any) {
    this.approvalDialogVisible = false;
    // call appropriate approve/reject proxy based on role; placeholder
    this.load();
  }
}
