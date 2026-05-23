import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SponsorshipService } from '../../services/sponsorship.service';
import { SponsorshipTypeService } from '../../services/sponsorship-type.service';
import { switchMap } from 'rxjs/operators';
import { of } from 'rxjs';

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
  error?: string;

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
    this.error = undefined;
    const dto = $event.dto;
    const save$ = this.requestId ? this.svc.update(this.requestId, dto) : this.svc.create(dto);

    save$
      .pipe(
        switchMap((created) => {
          const id = this.requestId || created.id;
          if ($event.submit && id) {
            return this.svc.submit(id).pipe(switchMap(() => of(id)));
          }
          return of(id);
        })
      )
      .subscribe({
        next: () => {
          this.loading = false;
          this.router.navigate(['/sponsorship/my-requests']);
        },
        error: (err) => {
          this.loading = false;
          this.error = err?.error?.error?.message || 'Failed to save request.';
        },
      });
  }
}
