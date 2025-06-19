using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace abpSourceCode.Data;

/* This is used if database provider does't define
 * IabpSourceCodeDbSchemaMigrator implementation.
 */
public class NullabpSourceCodeDbSchemaMigrator : IabpSourceCodeDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
