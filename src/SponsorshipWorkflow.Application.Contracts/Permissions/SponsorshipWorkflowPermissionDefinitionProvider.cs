using SponsorshipWorkflow.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace SponsorshipWorkflow.Permissions;

public class SponsorshipWorkflowPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SponsorshipWorkflowPermissions.GroupName);
        var requests = myGroup.AddPermission(SponsorshipWorkflowPermissions.SponsorshipRequests.Default, L("Permission:SponsorshipRequests"));
        requests.AddChild(SponsorshipWorkflowPermissions.SponsorshipRequests.Create, L("Permission:SponsorshipRequests.Create"));
        requests.AddChild(SponsorshipWorkflowPermissions.SponsorshipRequests.Edit, L("Permission:SponsorshipRequests.Edit"));
        requests.AddChild(SponsorshipWorkflowPermissions.SponsorshipRequests.Submit, L("Permission:SponsorshipRequests.Submit"));
        requests.AddChild(SponsorshipWorkflowPermissions.SponsorshipRequests.Cancel, L("Permission:SponsorshipRequests.Cancel"));
        requests.AddChild(SponsorshipWorkflowPermissions.SponsorshipRequests.ManagerApprove, L("Permission:SponsorshipRequests.ManagerApprove"));
        requests.AddChild(SponsorshipWorkflowPermissions.SponsorshipRequests.ManagerReject, L("Permission:SponsorshipRequests.ManagerReject"));
        requests.AddChild(SponsorshipWorkflowPermissions.SponsorshipRequests.FinanceApprove, L("Permission:SponsorshipRequests.FinanceApprove"));
        requests.AddChild(SponsorshipWorkflowPermissions.SponsorshipRequests.FinanceReject, L("Permission:SponsorshipRequests.FinanceReject"));
        requests.AddChild(SponsorshipWorkflowPermissions.SponsorshipRequests.ViewAll, L("Permission:SponsorshipRequests.ViewAll"));
        requests.AddChild(SponsorshipWorkflowPermissions.SponsorshipRequests.ViewWorkflowHistory, L("Permission:SponsorshipRequests.ViewWorkflowHistory"));

        var types = myGroup.AddPermission(SponsorshipWorkflowPermissions.SponsorshipTypes.Default, L("Permission:SponsorshipTypes"));
        types.AddChild(SponsorshipWorkflowPermissions.SponsorshipTypes.Manage, L("Permission:SponsorshipTypes.Manage"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SponsorshipWorkflowResource>(name);
    }
}
