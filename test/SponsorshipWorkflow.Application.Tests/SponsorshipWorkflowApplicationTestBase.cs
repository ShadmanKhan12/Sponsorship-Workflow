using Volo.Abp.Modularity;

namespace SponsorshipWorkflow;

public abstract class SponsorshipWorkflowApplicationTestBase<TStartupModule> : SponsorshipWorkflowTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
