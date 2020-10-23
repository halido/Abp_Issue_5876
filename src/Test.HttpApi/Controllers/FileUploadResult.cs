﻿namespace Test.Controllers
{
    public class FileUploadResult
    {
        public string FileUrl { get; set; }

        public FileUploadResult(string fileUrl,string fileName)
        {
            FileUrl = fileUrl;
            FileName = fileName;
        }

        public string FileName { get; set; }
    }
}