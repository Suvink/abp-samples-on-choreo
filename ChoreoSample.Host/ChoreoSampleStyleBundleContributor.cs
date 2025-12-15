using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace ChoreoSample;

public class ChoreoSampleStyleBundleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Add(new BundleFile("main.css", true));
    }
}