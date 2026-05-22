using SponsorshipWorkflow.Samples;
using Xunit;

namespace SponsorshipWorkflow.EntityFrameworkCore.Applications;

[Collection(SponsorshipWorkflowTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<SponsorshipWorkflowEntityFrameworkCoreTestModule>
{

}
