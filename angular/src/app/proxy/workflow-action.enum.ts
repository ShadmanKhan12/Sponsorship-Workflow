import { mapEnumToOptions } from '@abp/ng.core';

export enum WorkflowAction {
  Created = 0,
  Submitted = 1,
  ManagerApproved = 2,
  ManagerRejected = 3,
  FinanceApproved = 4,
  FinanceRejected = 5,
  Cancelled = 6,
  Updated = 7,
}

export const workflowActionOptions = mapEnumToOptions(WorkflowAction);
