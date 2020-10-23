using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Test.Files;
using Volo.Abp;
using Volo.Abp.Http;

namespace Test.Controllers
{
    [Route("api/files")]
    public class FilesController : TestController, IFileAppService
    {
        private readonly IFileAppService _fileAppService;

        public FilesController(IFileAppService fileAppService)
        {
            _fileAppService = fileAppService;
        }

        [HttpGet]
        [Route("{name}")]
        public Task<RawFileDto> GetAsync(string name) //TODO: output cache would be good
        {
            return _fileAppService.GetAsync(name);
        }

        [HttpGet]
        [Route("www/{name}")]
        public async Task<FileResult> GetForWebAsync(string name) //TODO: output cache would be good
        {
            var file = await _fileAppService.GetAsync(name);
            return File(
                file.Bytes,
                MimeTypes.GetByExtension(Path.GetExtension(name)),true
            );
        }

        [HttpPost]
        public Task<FileUploadOutputDto> CreateAsync(FileUploadInputDto input)
        {
            return _fileAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("upload")]
        public async Task<JsonResult> UploadImage(IFormFile file)
        {
            //TODO: localize exception messages

            if (file == null)
            {
                throw new UserFriendlyException("No file found!");
            }

            if (file.Length <= 0)
            {
                throw new UserFriendlyException("File is empty!");
            }
            // TODO : @maliming ADD break point here
            var output = await _fileAppService.CreateAsync(
                new FileUploadInputDto
                {
                    Bytes = file.GetAllBytes(),
                    Name = file.FileName
                }
            );

            return Json(new FileUploadResult(output.WebUrl,output.Name));
        }
    }
}
