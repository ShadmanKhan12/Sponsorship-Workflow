using Microsoft.Extensions.Localization;
using SponsorshipWorkflow.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace SponsorshipWorkflow;

[Dependency(ReplaceServices = true)]
public class SponsorshipWorkflowBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<SponsorshipWorkflowResource> _localizer;

    public SponsorshipWorkflowBrandingProvider(IStringLocalizer<SponsorshipWorkflowResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
