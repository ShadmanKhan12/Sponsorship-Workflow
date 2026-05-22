using SponsorshipWorkflow.Samples;
using Xunit;

namespace SponsorshipWorkflow.EntityFrameworkCore.Domains;

[Collection(SponsorshipWorkflowTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<SponsorshipWorkflowEntityFrameworkCoreTestModule>
{

}
