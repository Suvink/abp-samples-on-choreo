using ChoreoSample.Localization;
using Volo.Abp.AspNetCore.Components;

namespace ChoreoSample;

public abstract class ChoreoSampleComponentBase : AbpComponentBase
{
    protected ChoreoSampleComponentBase()
    {
        LocalizationResource = typeof(ChoreoSampleResource);
    }
}
