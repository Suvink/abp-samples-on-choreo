using Volo.Abp.Application.Services;
using ChoreoSample.Localization;

namespace ChoreoSample.Services;

/* Inherit your application services from this class. */
public abstract class ChoreoSampleAppService : ApplicationService
{
    protected ChoreoSampleAppService()
    {
        LocalizationResource = typeof(ChoreoSampleResource);
    }
}