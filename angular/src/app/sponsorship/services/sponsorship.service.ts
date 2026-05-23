import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { SponsorshipRequest } from '../models/request.model';
import { SponsorshipStatus } from '../enums/status.enum';
import { SponsorshipRequestService } from 'src/app/proxy/services/sponsorship-request.service';
import type { ApproveRejectRequestDto, CreateUpdateSponsorshipRequestDto, SponsorshipRequestDto } from 'src/app/proxy/dtos/models';

@Injectable({ providedIn: 'root' })
export class SponsorshipService {
  constructor(private proxy: SponsorshipRequestService) {}

  private mapListDtoToModel(dto: any): SponsorshipRequest {
    return {
      id: dto.id,
      title: dto.requestTitle || '',
      requestorName: dto.requestorName,
      department: dto.department,
      sponsorshipTypeId: dto.sponsorshipTypeId,
      requestedAmount: dto.requestedAmount,
      status: dto.status as SponsorshipStatus,
      createdAt: dto.submittedAt || dto.creationTime,
    } as SponsorshipRequest;
  }

  private mapDetailDtoToModel(dto: SponsorshipRequestDto): SponsorshipRequest {
    return {
      id: dto.id,
      title: dto.requestTitle || '',
      requestorName: dto.requestorName || '',
      department: dto.department,
      sponsorshipTypeId: dto.sponsorshipTypeId,
      eventName: dto.eventName || undefined,
      eventDate: dto.eventDate || undefined,
      requestedAmount: dto.requestedAmount,
      purpose: dto.purpose || undefined,
      expectedBusinessBenefit: dto.expectedBusinessBenefit || undefined,
      remarks: dto.remarks || undefined,
      status: dto.status as SponsorshipStatus,
      createdAt: dto.submittedAt || undefined,
    };
  }

  private toPayload(dto: any): CreateUpdateSponsorshipRequestDto {
    return {
      requestTitle: dto.title,
      requestorName: dto.requestorName,
      department: dto.department,
      sponsorshipTypeId: dto.sponsorshipTypeId,
      eventName: dto.eventName || null,
      eventDate: dto.eventDate || null,
      requestedAmount: dto.requestedAmount,
      purpose: dto.purpose || null,
      expectedBusinessBenefit: dto.expectedBusinessBenefit || null,
      remarks: dto.remarks || null,
    };
  }

  getMyRequests(): Observable<{ items: SponsorshipRequest[]; totalCount: number }> {
    return this.proxy.getMyRequests().pipe(
      map((res) => ({ items: (res || []).map((r) => this.mapListDtoToModel(r)), totalCount: (res || []).length })),
      catchError(() => of({ items: [], totalCount: 0 }))
    );
  }

  getAllRequests(skip = 0, take = 50): Observable<{ items: SponsorshipRequest[]; totalCount: number }> {
    return this.proxy.getAllRequests(skip, take).pipe(
      map((res) => ({
        items: (res.items || []).map((r) => this.mapListDtoToModel(r)),
        totalCount: res.totalCount ?? 0,
      })),
      catchError(() => of({ items: [], totalCount: 0 }))
    );
  }

  getById(id: string): Observable<SponsorshipRequest | null> {
    return this.proxy.get(id).pipe(
      map((dto) => (dto ? this.mapDetailDtoToModel(dto) : null)),
      catchError(() => of(null))
    );
  }

  getWorkflowHistory(id: string): Observable<any[]> {
    return this.proxy.getWorkflowHistory(id).pipe(
      map((items) => items || []),
      catchError(() => of([]))
    );
  }

  create(dto: any): Observable<SponsorshipRequestDto> {
    return this.proxy.create(this.toPayload(dto));
  }

  update(id: string, dto: any): Observable<SponsorshipRequestDto> {
    return this.proxy.updateDraft(id, this.toPayload(dto));
  }

  submit(id: string): Observable<void> {
    return this.proxy.submit(id);
  }

  cancel(id: string): Observable<void> {
    return this.proxy.cancel(id);
  }

  approveByManager(id: string, remarks?: string): Observable<void> {
    return this.proxy.approveByManager(id, { remarks } as ApproveRejectRequestDto);
  }

  rejectByManager(id: string, remarks?: string): Observable<void> {
    return this.proxy.rejectByManager(id, { remarks } as ApproveRejectRequestDto);
  }

  approveByFinance(id: string, remarks?: string): Observable<void> {
    return this.proxy.approveByFinance(id, { remarks } as ApproveRejectRequestDto);
  }

  rejectByFinance(id: string, remarks?: string): Observable<void> {
    return this.proxy.rejectByFinance(id, { remarks } as ApproveRejectRequestDto);
  }

  queryByStatus(status: SponsorshipStatus) {
    if (status === SponsorshipStatus.PendingManagerApproval) {
      return this.proxy.getPendingManagerApprovals().pipe(
        map((arr) => ({ items: (arr || []).map((r) => this.mapListDtoToModel(r)), totalCount: (arr || []).length })),
        catchError(() => of({ items: [], totalCount: 0 }))
      );
    }
    if (status === SponsorshipStatus.PendingFinanceReview) {
      return this.proxy.getPendingFinanceReviews().pipe(
        map((arr) => ({ items: (arr || []).map((r) => this.mapListDtoToModel(r)), totalCount: (arr || []).length })),
        catchError(() => of({ items: [], totalCount: 0 }))
      );
    }
    return this.getAllRequests(0, 100).pipe(
      map((res) => ({
        items: res.items.filter((i) => i.status === status),
        totalCount: res.items.filter((i) => i.status === status).length,
      }))
    );
  }
}
