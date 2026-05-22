using SponsorshipWorkflow.Localization;
using Volo.Abp.Application.Services;

namespace SponsorshipWorkflow;

/* Inherit your application services from this class.
 */
public abstract class SponsorshipWorkflowAppService : ApplicationService
{
    protected SponsorshipWorkflowAppService()
    {
        LocalizationResource = typeof(SponsorshipWorkflowResource);
    }
}
