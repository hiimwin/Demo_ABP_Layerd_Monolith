using abpSourceCode.Localization;
using Volo.Abp.Application.Services;

namespace abpSourceCode;

/* Inherit your application services from this class.
 */
public abstract class abpSourceCodeAppService : ApplicationService
{
    protected abpSourceCodeAppService()
    {
        LocalizationResource = typeof(abpSourceCodeResource);
    }
}
