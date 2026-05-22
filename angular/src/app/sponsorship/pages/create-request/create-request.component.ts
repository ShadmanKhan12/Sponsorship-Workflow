import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SponsorshipService } from '../../services/sponsorship.service';
import { SponsorshipTypeService } from '../../services/sponsorship-type.service';

@Component({
  selector: 'app-create-request',
  standalone: false,
  templateUrl: './create-request.component.html',
  styleUrls: ['./create-request.component.scss'],
})
export class CreateRequestPageComponent implements OnInit {
  requestId?: string | null = null;
  sponsorshipTypes: any[] = [];
  request: any = null;

  loading = false;

  constructor(
    private svc: SponsorshipService,
    private types: SponsorshipTypeService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.requestId = this.route.snapshot.queryParamMap.get('id');
    this.types.getAll().subscribe((r) => (this.sponsorshipTypes = r));
    if (this.requestId) {
      this.svc.getById(this.requestId).subscribe((r) => (this.request = r));
    }
  }

  onSave($event: { dto: any; submit?: boolean }) {
    this.loading = true;
    const dto = $event.dto;
    const action = this.requestId ? this.svc.update(this.requestId, dto) : this.svc.create(dto);
    action.subscribe(() => {
      if ($event.submit && this.requestId) {
        this.svc.submit(this.requestId).subscribe(() => this.router.navigate(['/sponsorship/my-requests']));
      } else {
        this.loading = false;
        this.router.navigate(['/sponsorship/my-requests']);
      }
    });
  }
}
