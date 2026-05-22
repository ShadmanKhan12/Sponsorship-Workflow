import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { SponsorshipRequest } from '../models/request.model';
import { SponsorshipStatus } from '../enums/status.enum';
import { SponsorshipRequestService } from 'src/app/proxy/services/sponsorship-request.service';

@Injectable({ providedIn: 'root' })
export class SponsorshipService {
  constructor(private proxy: SponsorshipRequestService) {}

  private mapListDtoToModel(dto: any) {
    return {
      id: dto.id,
      title: dto.requestTitle || '',
      requestorName: dto.requestorName,
      department: dto.department,
      sponsorshipTypeId: dto.sponsorshipTypeId,
      requestedAmount: dto.requestedAmount,
      status: dto.status as SponsorshipStatus,
      createdAt: dto.submittedAt || dto.createdAt,
    } as SponsorshipRequest;
  }

  getMyRequests(pagination?: any): Observable<{ items: SponsorshipRequest[]; totalCount: number }> {
    // proxy.getMyRequests does not accept pagination; pagination param is kept for compatibility
    return this.proxy.getMyRequests().pipe(
      map((res: any[]) => ({ items: (res || []).map((r) => this.mapListDtoToModel(r)), totalCount: (res || []).length })),
      catchError(() => of({ items: [], totalCount: 0 }))
    );
  }

  getById(id: string): Observable<SponsorshipRequest | null> {
    return this.proxy.getMyRequests().pipe(
      map((list: any[]) => {
        const found = (list || []).find((i) => i.id === id);
        return found ? this.mapListDtoToModel(found) : null;
      }),
      catchError(() => of(null))
    );
  }

  create(dto: any): Observable<any> {
    const payload = {
      requestTitle: dto.title,
      requestorName: dto.requestorName,
      department: dto.department,
      sponsorshipTypeId: dto.sponsorshipTypeId,
      eventName: dto.eventName,
      eventDate: dto.eventDate,
      requestedAmount: dto.requestedAmount,
      purpose: dto.purpose,
      expectedBusinessBenefit: dto.expectedBusinessBenefit,
      remarks: dto.remarks,
    };
    return this.proxy.create(payload).pipe(catchError((err) => { throw err; }));
  }

  update(id: string, dto: any): Observable<any> {
    const payload = {
      requestTitle: dto.title,
      requestorName: dto.requestorName,
      department: dto.department,
      sponsorshipTypeId: dto.sponsorshipTypeId,
      eventName: dto.eventName,
      eventDate: dto.eventDate,
      requestedAmount: dto.requestedAmount,
      purpose: dto.purpose,
      expectedBusinessBenefit: dto.expectedBusinessBenefit,
      remarks: dto.remarks,
    };
    return this.proxy.updateDraft(id, payload).pipe(catchError((err) => { throw err; }));
  }

  submit(id: string): Observable<any> {
    return this.proxy.submit(id).pipe(catchError((err) => { throw err; }));
  }

  cancel(id: string): Observable<any> {
    return this.proxy.cancel(id).pipe(catchError((err) => { throw err; }));
  }

  queryByStatus(status: SponsorshipStatus, paging?: any) {
    if (status === SponsorshipStatus.PendingManagerApproval) {
      return this.proxy.getPendingManagerApprovals().pipe(map((arr: any[]) => ({ items: (arr || []).map((r) => this.mapListDtoToModel(r)), totalCount: (arr || []).length })), catchError(() => of({ items: [], totalCount: 0 })));
    }
    if (status === SponsorshipStatus.PendingFinanceReview) {
      return this.proxy.getPendingFinanceReviews().pipe(map((arr: any[]) => ({ items: (arr || []).map((r) => this.mapListDtoToModel(r)), totalCount: (arr || []).length })), catchError(() => of({ items: [], totalCount: 0 })));
    }
    // Fallback: call getAllRequests and filter by status
    return this.proxy.getAllRequests(0, paging?.size || 50).pipe(
      map((res: any) => {
        const items = (res.items || []).filter((i: any) => i.status === status).map((r: any) => this.mapListDtoToModel(r));
        return { items, totalCount: items.length };
      }),
      catchError(() => of({ items: [], totalCount: 0 }))
    );
  }
}
