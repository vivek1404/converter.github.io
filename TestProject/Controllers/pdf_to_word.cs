using Microsoft.AspNetCore.Mvc;
using SautinSoft;
using SolrNet.Utils;
using TestProject.Models;
using FileModel = TestProject.Models.FileModel;
using VisioForge.Libs.MediaFoundation.OPM;

//using VisioForge.Shared.MediaFoundation.OPM;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("[Controller]")]


    public class pdf_to_word : ControllerBase


    {
        private readonly string AppDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        [HttpPost]
        public async Task<OkResult> Main([FromForm] FileModel model)
        //static void Main(string[] args)
        {
            SautinSoft.PdfFocus f = new PdfFocus();
            FileRecord file = await SaveFileAsync(model.MyFile);
            f.OpenPdf(file.FilePath);



            f.ToWord(@"C:\Users\vivek.kumar2\Downloads\output.docx");
            return Ok();
        }


        private async Task<FileRecord> SaveFileAsync(IFormFile myFile)
        {
            FileRecord file = new FileRecord();
            if (myFile != null)
            {
                if (!Directory.Exists(AppDirectory))
                    Directory.CreateDirectory(AppDirectory);

                // var fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(myFile.FileName);
                var fileName = myFile.FileName;//DateTime.Now.Ticks.ToString() + Path.GetExtension(myFile.FileName);
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
    //public static async Task<Stream> GetStream(this IFormFile formFile)
    //{
    //    using (var memoryStream = new MemoryStream())
    //    {
    //        await formFile.CopyToAsync(memoryStream);
    //        return memoryStream;
    //    }
    //}

    
}
