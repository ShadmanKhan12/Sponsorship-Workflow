import { Component, OnInit } from '@angular/core';
import { SponsorshipService } from '../../services/sponsorship.service';

@Component({
  selector: 'app-admin-dashboard',
  standalone: false,
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.scss'],
})
export class AdminDashboardPageComponent implements OnInit {
  items: any[] = [];
  filters = { status: '', department: '', sponsorshipTypeId: '' };

  constructor(private svc: SponsorshipService) {}

  ngOnInit(): void {
    this.load();
  }

  load() {
    // Would pass filters into proxy; placeholder implementation
    this.svc.getAllRequests(0, 50).subscribe((r) => (this.items = r.items || []));
  }
}
