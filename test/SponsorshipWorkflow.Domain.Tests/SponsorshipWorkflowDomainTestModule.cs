using Volo.Abp.Modularity;

namespace SponsorshipWorkflow;

[DependsOn(
    typeof(SponsorshipWorkflowDomainModule),
    typeof(SponsorshipWorkflowTestBaseModule)
)]
public class SponsorshipWorkflowDomainTestModule : AbpModule
{

}
