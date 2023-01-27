using LovePdf.Core;
using LovePdf.Model.Task;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TestProject.Models;
using FileModel = TestProject.Models.FileModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace TestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Organize_pdf : ControllerBase
    {
        private readonly string AppDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        //private static List<FileRecord> file = new List<FileRecord>();
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> convert_Organize_pdf([FromForm] FileModel model)
        {
            try
            {
                //FileRecord file = await SaveFileAsync(model.MyFile);
                var lovePdfAPi = new LovePdfApi("project_public_7a472b8b4521b3fd9afd3152313a154a_Vnnae2a5cfbe46a7b5c02d3fdaa91d4178e0f", "secret_key_19c30ad4f9f9d63dccbb7be1d8643927_2qTnVb462575c698cf90f47bee444ef80ead7");
                var task = lovePdfAPi.CreateTask<PageNumbersTask>();
                FileRecord file = await SaveFileAsync(model.MyFile);
                //var file = task.AddFile("C:\\Users\\vivek.kumar2\\source\\repos\\Practice\\Practice\\Vivek's Resume.pdf");
                //FileRecord file = AddFile(model.MyFile);

                //FileRecord file = await SaveFileAsync(model.MyFile);



                var files = task.AddFile(file.FilePath);
                //var files = task.AddFile(model.MyFile);
                var time = task.Process();
                //task.DownloadFile("C:\\Users\\vivek.kumar2\\source\\repos\\Practice");
                string pathToDownload = "C:\\Users\\vivek.kumar2\\Downloads\\output.zip";
                task.DownloadFile("C:\\Users\\vivek.kumar2\\Downloads");

                if (!Directory.Exists(AppDirectory))
                    Directory.CreateDirectory(AppDirectory);

                var path = Path.Combine(AppDirectory, pathToDownload);

                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                var contentType = "APPLICATION/octet-stream";
                var fileName = Path.GetFileName(path);

                return File(memory, contentType, fileName);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

        }
        private async Task<FileRecord> SaveFileAsync(IFormFile myFile)
        {
            FileRecord file = new FileRecord();
            if (myFile != null)
            {
                if (!Directory.Exists(AppDirectory))
                    Directory.CreateDirectory(AppDirectory);

                var fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(myFile.FileName);
                var path = Path.Combine(AppDirectory, fileName);

                //file.Id = fileDB.Count() + 1;
                file.FilePath = path;
                file.FileName = fileName;
                file.FileFormat = Path.GetExtension(myFile.FileName);
                file.ContentType = myFile.ContentType;

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await myFile.CopyToAsync(stream);
                }

                return file;
            }
            return file;
        }


    }
}
