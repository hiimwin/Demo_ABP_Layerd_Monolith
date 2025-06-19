using System.Threading.Tasks;

namespace abpSourceCode.Data;

public interface IabpSourceCodeDbSchemaMigrator
{
    Task MigrateAsync();
}
