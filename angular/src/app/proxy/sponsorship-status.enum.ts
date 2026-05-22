import { mapEnumToOptions } from '@abp/ng.core';

export enum SponsorshipStatus {
  Draft = 0,
  PendingManagerApproval = 1,
  PendingFinanceReview = 2,
  Approved = 3,
  Rejected = 4,
  Cancelled = 5,
}

export const sponsorshipStatusOptions = mapEnumToOptions(SponsorshipStatus);
