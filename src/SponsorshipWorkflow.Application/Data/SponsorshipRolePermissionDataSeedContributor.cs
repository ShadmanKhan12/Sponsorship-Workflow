using System.Threading.Tasks;
using SponsorshipWorkflow.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace SponsorshipWorkflow.Data;

public class SponsorshipRolePermissionDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IPermissionManager _permissionManager;

    public SponsorshipRolePermissionDataSeedContributor(IPermissionManager permissionManager)
    {
        _permissionManager = permissionManager;
    }

    [UnitOfWork]
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        await GrantRequestorAsync();
        await GrantManagerAsync();
        await GrantFinanceAsync();
        await GrantSystemAdminAsync();
    }

    private async Task GrantRequestorAsync()
    {
        const string role = "Requestor";
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.Default);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.Create);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.Edit);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.Submit);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.Cancel);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.ViewWorkflowHistory);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipTypes.Default);
    }

    private async Task GrantManagerAsync()
    {
        const string role = "Manager";
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.Default);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.ManagerApprove);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.ManagerReject);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.ViewWorkflowHistory);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipTypes.Default);
    }

    private async Task GrantFinanceAsync()
    {
        const string role = "FinanceAdmin";
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.Default);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.FinanceApprove);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.FinanceReject);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.ViewWorkflowHistory);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipTypes.Default);
    }

    private async Task GrantSystemAdminAsync()
    {
        const string role = "SystemAdmin";
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.Default);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.Create);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.Edit);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.Submit);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.Cancel);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.ManagerApprove);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.ManagerReject);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.FinanceApprove);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.FinanceReject);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.ViewAll);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipRequests.ViewWorkflowHistory);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipTypes.Default);
        await GrantAsync(role, SponsorshipWorkflowPermissions.SponsorshipTypes.Manage);
    }

    private Task GrantAsync(string roleName, string permissionName)
    {
        return _permissionManager.SetForRoleAsync(roleName, permissionName, true);
    }
}
