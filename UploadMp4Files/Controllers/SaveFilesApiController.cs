using Microsoft.AspNetCore.Mvc;
using UploadMp4Files.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UploadMp4Files.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveFilesApiController : ControllerBase
    {

        private readonly IFileService _service;
        
        public SaveFilesApiController(IFileService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> SaveMp4Files(IFormFile file)
        {

            string path = _service.CreateFilesUploadFolder();


            string fileNameWithPath = Path.Combine(path, file.FileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }


            return Ok("MP4 files have been uploaded to the media folder successfully.");



        }

    }
}
