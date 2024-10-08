using UploadMp4Files.Models;
using System.IO;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UploadMp4Files.Services
{
    public class FileService : IFileService
    {

        private readonly string[] _fileSizeUnit = { "Bytes", "KB", "MB", "GB", "TB" };
       
        
        public string CreateFilesUploadFolder()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/mp4files");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public List<FileModel> GetUploadedFiles()
        {
            string[] files = Directory.GetFiles(CreateFilesUploadFolder());
            if (files.Length > 0)
            {
                return files.Where(f=>Path.GetExtension(f)==".mp4").Select(f => new FileModel()
                {
                    FileName = Path.GetFileName(f),
                    FilePath = Path.Combine("~/mp4files/", Path.GetFileName(f)),
                    FileSize = new FileInfo(f).Length,
                    DisplayFileSize=ConvertFileSize(new FileInfo(f).Length)
                }).ToList();
            }

            return new List<FileModel>();
        }

        public string ConvertFileSize(long fileSize)
        {
            
            if (fileSize ==0)
            {
                return "0 Bytes";
            }
            int unitIndex = 0;
            double size=Convert.ToDouble(fileSize);

            while (size>=1024d && unitIndex<_fileSizeUnit.Length-1)
            {
                unitIndex++;
                size /= 1024;
            }

            return string.Format(CultureInfo.InvariantCulture,"{0:0.##} {1}", size, _fileSizeUnit[unitIndex]);

        }
    }
}
