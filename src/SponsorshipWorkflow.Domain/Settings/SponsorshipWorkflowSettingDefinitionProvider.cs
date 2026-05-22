using Volo.Abp.Settings;

namespace SponsorshipWorkflow.Settings;

public class SponsorshipWorkflowSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(SponsorshipWorkflowSettings.MySetting1));
    }
}
