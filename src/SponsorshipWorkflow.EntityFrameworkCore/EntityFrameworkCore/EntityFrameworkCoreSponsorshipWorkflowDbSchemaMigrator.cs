using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SponsorshipWorkflow.Data;
using Volo.Abp.DependencyInjection;

namespace SponsorshipWorkflow.EntityFrameworkCore;

public class EntityFrameworkCoreSponsorshipWorkflowDbSchemaMigrator
    : ISponsorshipWorkflowDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreSponsorshipWorkflowDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the SponsorshipWorkflowDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<SponsorshipWorkflowDbContext>()
            .Database
            .MigrateAsync();
    }
}
