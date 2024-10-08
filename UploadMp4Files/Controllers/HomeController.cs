using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using UploadMp4Files.Models;
using UploadMp4Files.Services;

namespace UploadMp4Files.Controllers
{
    public class HomeController : Controller
    {
        //file size 200MB
        private readonly long _maxFileSize = 209715200;
        private readonly IFileService _service;

        public HomeController(IFileService service)
        {
            _service = service;
        }



        public IActionResult Index()
        {
            var model = new MultipleFilesModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult MultiUpload(MultipleFilesModel model)
        {
            model.IsResponse = true;
            if (model.Files.Count > 0)
            {
                var mp4Files = model.Files.Where(f => Path.GetExtension(f.FileName) == ".mp4").ToList();
                string apiUrl = "https://localhost:7039/api/SaveFilesApi";

                foreach (var file in model.Files)
                {
                    //If the file size is greater than 200MB, then it won't be saved
                    if (file.Length > _maxFileSize)
                    {
                        continue;
                    }
                    HttpClient client = new();

                    var content = new MultipartFormDataContent();
                    var fileContent = new StreamContent(file.OpenReadStream());
                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);
                    content.Add(fileContent, "file", file.FileName);
                    HttpResponseMessage response = client.PostAsync(apiUrl, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        model.IsSuccess = true;
                        model.Message = "Files upload successfully";
                    }
                    else
                    {
                        model.IsSuccess = false;
                        model.Message = "Please select files";

                    }
                }
            }
            else
            {
                model.IsSuccess = false;
                model.Message = "Please select files";
            }



            return View("Index", model);
        }

        public IActionResult DisplayUploadeFiles()
        {


            var files = _service.GetUploadedFiles();
            return View(files);



        }


    }
}
