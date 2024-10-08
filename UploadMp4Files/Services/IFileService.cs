using UploadMp4Files.Models;

namespace UploadMp4Files.Services
{
    public interface IFileService
    {
        string CreateFilesUploadFolder();

        List<FileModel> GetUploadedFiles();

        /// <summary>
        /// Convert file size from long to Bytes/KB/MB/GB/TB
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>

        string ConvertFileSize(long fileSize);

    }
}
