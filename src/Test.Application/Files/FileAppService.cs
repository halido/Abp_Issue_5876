using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.BlobStoring;
using Volo.Abp.Validation;

namespace Test.Files
{
    [RemoteService(isEnabled:false,IsMetadataEnabled = false)]
    public class FileAppService : TestAppService, IFileAppService
    {
        protected IBlobContainer<VideoContainer> BlobContainer { get; }

        public FileAppService(
            IBlobContainer<VideoContainer> blobContainer)
        {
            BlobContainer = blobContainer;
        }

        public virtual async Task<RawFileDto> GetAsync(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            return new RawFileDto
            {
                Bytes = await BlobContainer.GetAllBytesAsync(name)
            };
        }
        public virtual async Task<FileUploadOutputDto> CreateAsync(FileUploadInputDto input)
        {
            // TODO : @maliming ADD break point here
            if (input.Bytes.IsNullOrEmpty())
            {
                ThrowValidationException("Bytes can not be null or empty!", "Bytes");
            }

            if (input.Bytes.Length > TestConsts.MaxFileSize)
            {
                throw new UserFriendlyException($"File exceeds the maximum upload size ({TestConsts.MaxFileSizeAsMegabytes} MB)!");
            }


            var uniqueFileName = GenerateUniqueFileName(Path.GetExtension(input.Name));

            await BlobContainer.SaveAsync(uniqueFileName, input.Bytes);

            return new FileUploadOutputDto
            {
                Name = uniqueFileName,
                WebUrl = TestConsts.VideoBasePath + uniqueFileName
            };
        }

        private static void ThrowValidationException(string message, string memberName)
        {
            throw new AbpValidationException(message,
                new List<ValidationResult>
                {
                    new ValidationResult(message, new[] {memberName})
                });
        }

        protected virtual string GenerateUniqueFileName(string extension, string prefix = null, string postfix = null)
        {
            return prefix + GuidGenerator.Create().ToString("N") + postfix + extension;
        }
    }
}
