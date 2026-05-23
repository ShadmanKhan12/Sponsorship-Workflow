/** Must match SponsorshipWorkflowPermissions in the backend. */
export const SponsorshipPermissions = {
  requests: {
    default: 'SponsorshipWorkflow.SponsorshipRequests',
    create: 'SponsorshipWorkflow.SponsorshipRequests.Create',
    viewAll: 'SponsorshipWorkflow.SponsorshipRequests.ViewAll',
    managerApprove: 'SponsorshipWorkflow.SponsorshipRequests.ManagerApprove',
    financeApprove: 'SponsorshipWorkflow.SponsorshipRequests.FinanceApprove',
    viewWorkflowHistory: 'SponsorshipWorkflow.SponsorshipRequests.ViewWorkflowHistory',
  },
  types: {
    manage: 'SponsorshipWorkflow.SponsorshipTypes.Manage',
  },
} as const;
