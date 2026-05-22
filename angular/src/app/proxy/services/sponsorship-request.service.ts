import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable, inject } from '@angular/core';
import type { ApproveRejectRequestDto, CreateUpdateSponsorshipRequestDto, SponsorshipRequestDto, SponsorshipRequestListDto, WorkflowHistoryDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class SponsorshipRequestService {
  private restService = inject(RestService);
  apiName = 'Default';
  

  approveByFinance = (id: string, input: ApproveRejectRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/sponsorship-request/${id}/approve-by-finance`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  approveByManager = (id: string, input: ApproveRejectRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/sponsorship-request/${id}/approve-by-manager`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  cancel = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/sponsorship-request/${id}/cancel`,
    },
    { apiName: this.apiName,...config });
  

  create = (input: CreateUpdateSponsorshipRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SponsorshipRequestDto>({
      method: 'POST',
      url: '/api/app/sponsorship-request',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  getAllRequests = (skip?: number, take: number = 20, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<SponsorshipRequestListDto>>({
      method: 'GET',
      url: '/api/app/sponsorship-request/requests',
      params: { skip, take },
    },
    { apiName: this.apiName,...config });
  

  getMyRequests = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, SponsorshipRequestListDto[]>({
      method: 'GET',
      url: '/api/app/sponsorship-request/my-requests',
    },
    { apiName: this.apiName,...config });
  

  getPendingFinanceReviews = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, SponsorshipRequestListDto[]>({
      method: 'GET',
      url: '/api/app/sponsorship-request/pending-finance-reviews',
    },
    { apiName: this.apiName,...config });
  

  getPendingManagerApprovals = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, SponsorshipRequestListDto[]>({
      method: 'GET',
      url: '/api/app/sponsorship-request/pending-manager-approvals',
    },
    { apiName: this.apiName,...config });
  

  getWorkflowHistory = (sponsorshipRequestId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, WorkflowHistoryDto[]>({
      method: 'GET',
      url: `/api/app/sponsorship-request/workflow-history/${sponsorshipRequestId}`,
    },
    { apiName: this.apiName,...config });
  

  rejectByFinance = (id: string, input: ApproveRejectRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/sponsorship-request/${id}/reject-by-finance`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  rejectByManager = (id: string, input: ApproveRejectRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/sponsorship-request/${id}/reject-by-manager`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  submit = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/sponsorship-request/${id}/submit`,
    },
    { apiName: this.apiName,...config });
  

  updateDraft = (id: string, input: CreateUpdateSponsorshipRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SponsorshipRequestDto>({
      method: 'PUT',
      url: `/api/app/sponsorship-request/${id}/draft`,
      body: input,
    },
    { apiName: this.apiName,...config });
}