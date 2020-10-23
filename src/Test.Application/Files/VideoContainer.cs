using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace Test.Files
{
    [BlobContainerName("videos")]
    public class VideoContainer:ITransientDependency
    {

    }
}
