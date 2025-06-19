using abpSourceCode.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace abpSourceCode.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(abpSourceCodeEntityFrameworkCoreModule),
    typeof(abpSourceCodeApplicationContractsModule)
)]
public class abpSourceCodeDbMigratorModule : AbpModule
{
}
