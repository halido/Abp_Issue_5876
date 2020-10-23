using Test.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Test
{
    [DependsOn(
        typeof(TestEntityFrameworkCoreTestModule)
        )]
    public class TestDomainTestModule : AbpModule
    {

    }
}