using System.Threading.Tasks;

namespace SponsorshipWorkflow.Data;

public interface ISponsorshipWorkflowDbSchemaMigrator
{
    Task MigrateAsync();
}
