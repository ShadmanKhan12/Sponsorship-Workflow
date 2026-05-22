namespace SponsorshipWorkflow.Permissions;

public static class SponsorshipWorkflowPermissions
{
    public const string GroupName = "SponsorshipWorkflow";
    public static class SponsorshipRequests
    {
        public const string Default = GroupName + ".SponsorshipRequests";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Submit = Default + ".Submit";
        public const string Cancel = Default + ".Cancel";
        public const string ManagerApprove = Default + ".ManagerApprove";
        public const string ManagerReject = Default + ".ManagerReject";
        public const string FinanceApprove = Default + ".FinanceApprove";
        public const string FinanceReject = Default + ".FinanceReject";
        public const string ViewAll = Default + ".ViewAll";
        public const string ViewWorkflowHistory = Default + ".ViewWorkflowHistory";
    }

    public static class SponsorshipTypes
    {
        public const string Default = GroupName + ".SponsorshipTypes";
        public const string Manage = Default + ".Manage";
    }
}
