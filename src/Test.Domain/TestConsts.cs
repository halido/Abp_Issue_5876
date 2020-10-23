using System;

namespace Test
{
    public static class TestConsts
    {
        public const string DbTablePrefix = "App";

        public const string DbSchema = null;
        public static int MaxFileSize { get; set; } = 52428800 ; //50MB
        public static int MaxFileSizeAsMegabytes => Convert.ToInt32((MaxFileSize / 1024f) / 1024f);
        
        public const string VideoBasePath = "/api/files/www/";
    }
}
