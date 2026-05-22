using Volo.Abp.Modularity;

namespace SponsorshipWorkflow;

[DependsOn(
    typeof(SponsorshipWorkflowApplicationModule),
    typeof(SponsorshipWorkflowDomainTestModule)
)]
public class SponsorshipWorkflowApplicationTestModule : AbpModule
{

}
