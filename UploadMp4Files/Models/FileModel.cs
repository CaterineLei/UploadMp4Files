using System.ComponentModel;

namespace UploadMp4Files.Models
{
    public class FileModel
    {
        [DisplayName("File Name")]
        public string FileName { get; set; }
        public long FileSize { get; set; }

        [DisplayName("File Size")]
        public string DisplayFileSize { get; set; }
        public string FilePath { get; set; }
    }
}
