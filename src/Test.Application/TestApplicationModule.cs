using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace Test
{
    [DependsOn(
        typeof(TestDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(TestApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule), typeof(AbpBlobStoringFileSystemModule)
    )]
    public class TestApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            
            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<TestApplicationModule>(); });

            
            var wwwRoot = context.Services.GetConfiguration().GetValue<string>(WebHostDefaults.ContentRootKey);
            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.ConfigureAll((containerName, containerConfiguration) =>
                {
                    containerConfiguration.UseFileSystem(f => { f.BasePath = Path.Combine(wwwRoot, "uploads"); });
                });
            });
        }
    }
}