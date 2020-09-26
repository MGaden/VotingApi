using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Voting.Shared
{
    public class FileHelper
    {
        public static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        public static bool ValidFileExtension(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            if (types.ContainsKey(ext))
                return true;
            return false;
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                //{".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},  
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".ppt", "application/vnd.ms-powerpoint"},
                {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"}
                //{".gif", "image/gif"},
                //{".csv", "text/csv"}
            };
        }

        public static string GetFilePath(string filename)
        {
            return Path.Combine(GetUploadDirectory(), filename);
        }

        public static string GetUploadDirectory()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            Directory.CreateDirectory(path);
            return path;
        }

        public static void DeleteFile(string attachmentUrl)
        {
            if(File.Exists(GetFilePath(attachmentUrl)))
            File.Delete(GetFilePath(attachmentUrl));
        }
    }
}
