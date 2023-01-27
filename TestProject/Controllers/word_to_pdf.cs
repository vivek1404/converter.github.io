using Microsoft.AspNetCore.Mvc;
//using Spire.Presentation.Converter.Equation.Word;
using TestProject.Models;
//using FileFormat = Spire.Pdf.FileFormat;
using FileModel = TestProject.Models.FileModel;
//using System.Reflection.Metadata;
using Spire.Doc;
using Spire.Doc.Documents;



namespace TestProject.Controllers
{
    [ApiController]
    [Route("[Controller]")]


    public class word_to_pdf : ControllerBase
    {
        //static void Main(string[] args
        private readonly string AppDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        [HttpPost]
        public async Task<OkResult> Main([FromForm] FileModel model)
        {
            //Create a Presentation instance
            Document document = new Document();
            //Load a PowerPoint Presentation
            //ppt.LoadFromFile(@"Sample.pptx");
            FileRecord file = await SaveFileAsync(model.MyFile);
            //ppt.LoadFromFile(file.FilePath);
            document.LoadFromFile(file.FilePath);

            //Save it to PDF
            //ppt.SaveToFile("C:\\Users\\vivek.kumar2\\Downloads\\ToPdf1.pdf", FileFormat.PDF);
            document.SaveToFile("C:\\Users\\vivek.kumar2\\Downloads\\toPDF.PDF", FileFormat.PDF);
            //System.Diagnostics.Process.Start("toPDF.PDF");
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