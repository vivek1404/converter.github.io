using Microsoft.AspNetCore.Mvc;
using Spire.Pdf;

using Spire.Presentation;
using TestProject.Models;
using FileFormat = Spire.Pdf.FileFormat;
using FileModel = TestProject.Models.FileModel;


namespace TestProject.Controllers
{
    [ApiController]
    [Route("[Controller]")]


    public class pdf_to_ppt : ControllerBase
    {
        //static void Main(string[] args
        private readonly string AppDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        [HttpPost]
        public async Task<OkResult> Main([FromForm] FileModel model)
        {
            //Create a Presentation instance
            PdfDocument pdf = new PdfDocument();
            //Load a PowerPoint Presentation
            //ppt.LoadFromFile(@"Sample.pptx");
            FileRecord file = await SaveFileAsync(model.MyFile);
            //ppt.LoadFromFile(file.FilePath);
            pdf.LoadFromFile(file.FilePath);

            //Save it to PDF
            //ppt.SaveToFile("C:\\Users\\vivek.kumar2\\Downloads\\ToPdf1.pdf", FileFormat.PDF);
            pdf.SaveToFile("C:\\Users\\vivek.kumar2\\Downloads\\ConvertPDFtoPowerPoint.pptx", FileFormat.PPTX);
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
}