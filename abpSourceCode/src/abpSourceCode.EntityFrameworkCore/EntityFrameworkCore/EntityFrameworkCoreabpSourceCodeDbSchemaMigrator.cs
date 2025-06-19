using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using abpSourceCode.Data;
using Volo.Abp.DependencyInjection;

namespace abpSourceCode.EntityFrameworkCore;

public class EntityFrameworkCoreabpSourceCodeDbSchemaMigrator
    : IabpSourceCodeDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreabpSourceCodeDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the abpSourceCodeDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<abpSourceCodeDbContext>()
            .Database
            .MigrateAsync();
    }
}
