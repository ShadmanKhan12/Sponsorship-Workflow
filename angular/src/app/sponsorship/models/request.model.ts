import { SponsorshipStatus } from '../enums/status.enum';

export interface SponsorshipRequest {
  id?: string;
  title: string;
  requestorName: string;
  department?: string;
  sponsorshipTypeId?: string;
  sponsorshipTypeName?: string;
  eventName?: string;
  eventDate?: string;
  requestedAmount?: number;
  purpose?: string;
  expectedBusinessBenefit?: string;
  remarks?: string;
  status?: SponsorshipStatus;
  createdAt?: string;
}
