using SponsorshipWorkflow.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace SponsorshipWorkflow.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SponsorshipWorkflowEntityFrameworkCoreModule),
    typeof(SponsorshipWorkflowApplicationContractsModule)
)]
public class SponsorshipWorkflowDbMigratorModule : AbpModule
{
}
