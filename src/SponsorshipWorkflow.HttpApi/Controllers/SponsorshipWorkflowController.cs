using SponsorshipWorkflow.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace SponsorshipWorkflow.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SponsorshipWorkflowController : AbpControllerBase
{
    protected SponsorshipWorkflowController()
    {
        LocalizationResource = typeof(SponsorshipWorkflowResource);
    }
}
