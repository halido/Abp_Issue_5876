using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Test.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(TestHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class TestConsoleApiClientModule : AbpModule
    {
        
    }
}
