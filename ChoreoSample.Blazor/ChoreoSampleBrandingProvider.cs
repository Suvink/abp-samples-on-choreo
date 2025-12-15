using Microsoft.Extensions.Localization;
using ChoreoSample.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ChoreoSample;

[Dependency(ReplaceServices = true)]
public class ChoreoSampleBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<ChoreoSampleResource> _localizer;

    public ChoreoSampleBrandingProvider(IStringLocalizer<ChoreoSampleResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}