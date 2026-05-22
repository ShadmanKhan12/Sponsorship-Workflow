import type { SponsorshipStatus } from '../sponsorship-status.enum';
import type { WorkflowAction } from '../workflow-action.enum';

export interface ApproveRejectRequestDto {
  remarks?: string | null;
}

export interface CreateUpdateSponsorshipRequestDto {
  requestTitle: string;
  requestorName: string;
  department: string;
  sponsorshipTypeId: string;
  eventName?: string | null;
  eventDate?: string | null;
  requestedAmount?: number;
  purpose?: string | null;
  expectedBusinessBenefit?: string | null;
  remarks?: string | null;
}

export interface CreateUpdateSponsorshipTypeDto {
  name: string;
  description?: string | null;
  isActive?: boolean;
}

export interface SponsorshipRequestDto {
  id?: string;
  requestTitle?: string;
  requestorName?: string;
  department?: string;
  sponsorshipTypeId?: string;
  eventName?: string | null;
  eventDate?: string | null;
  requestedAmount?: number;
  purpose?: string | null;
  expectedBusinessBenefit?: string | null;
  remarks?: string | null;
  managerRemarks?: string | null;
  financeRemarks?: string | null;
  status?: SponsorshipStatus;
  submittedAt?: string | null;
  approvedAt?: string | null;
  cancelledAt?: string | null;
}

export interface SponsorshipRequestListDto {
  id?: string;
  requestTitle?: string;
  requestorName?: string;
  status?: SponsorshipStatus;
  requestedAmount?: number;
  submittedAt?: string | null;
}

export interface SponsorshipTypeDto {
  id?: string;
  name?: string;
  description?: string | null;
  isActive?: boolean;
}

export interface WorkflowHistoryDto {
  id?: string;
  sponsorshipRequestId?: string;
  action?: WorkflowAction;
  previousStatus?: SponsorshipStatus;
  newStatus?: SponsorshipStatus;
  remarks?: string | null;
  performedByUserId?: string | null;
  performedByUserName?: string | null;
  performedAt?: string;
}
