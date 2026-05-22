using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace SponsorshipWorkflow.Data;

/* This is used if database provider does't define
 * ISponsorshipWorkflowDbSchemaMigrator implementation.
 */
public class NullSponsorshipWorkflowDbSchemaMigrator : ISponsorshipWorkflowDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
