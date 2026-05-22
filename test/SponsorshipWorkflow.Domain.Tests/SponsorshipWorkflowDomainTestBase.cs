using Volo.Abp.Modularity;

namespace SponsorshipWorkflow;

/* Inherit from this class for your domain layer tests. */
public abstract class SponsorshipWorkflowDomainTestBase<TStartupModule> : SponsorshipWorkflowTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
