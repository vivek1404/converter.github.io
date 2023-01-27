
using Aspose.Slides;
using Aspose.Slides.Export;
using Microsoft.AspNetCore.Mvc;

using TestProject.Models;
using FileModel = TestProject.Models.FileModel;
//using FileFormat = Spire.Presentation.FileFormat;
//using System.IO.Stream;



namespace TestProject.Controllers
{
    [ApiController]
    [Route("[Controller]")]


    public class powerpoint_to_pdf : ControllerBase
    {
        //static void Main(string[] args
        private readonly string AppDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        [HttpPost]
        public async Task<OkResult> Main([FromForm] FileModel model)
        {
            //Create a Presentation instance
            //Presentation ppt = new Presentation();
            //Presentation ppt = new Presentation();

            ////Load a PowerPoint Presentation
            ////ppt.LoadFromFile(@"Sample.pptx");
            //
            //ppt.LoadFromFile(file.FilePath);

            ////Save it to PDF
            //ppt.SaveToFile("C:\\Users\\vivek.kumar2\\Downloads\\ToPdf1.pdf", FileFormat.PDF);




            //Opens a PowerPoint Presentation
            //FileRecord file = await SaveFileAsync(model.MyFile);
            //IPresentation presentation = Presentation.Open("C:\\Users\\vivek.kumar2\\source\\repos\\Practice\\Practice\\samplepptx.pptx");
            ////Converts the PowerPoint Presentation into PDF document
            //PdfDocument pdfDocument = PresentationToPdfConverter.Convert(presentation);
            ////Saves the PDF document
            //pdfDocument.Save("PPTToPDF.pdf");
            ////Closes the PDF document
            //pdfDocument.Close(true);
            ////Closes the Presentation
            //presentation.Close();
            ////This will open the PDF file so, the result will be seen in default PDF viewer
            //System.Diagnostics.Process.Start("PPTToPDF.pdf");

            // Instantiate a Presentation object that represents a PPTX file
            FileRecord file = await SaveFileAsync(model.MyFile);
            Presentation presentation = new Presentation(file.FilePath);

            // Instantiate the PdfOptions class
            PdfOptions pdfOptions = new PdfOptions();

            // Set Jpeg quality
            pdfOptions.JpegQuality = 90;

            // Set behavior for metafiles
            pdfOptions.SaveMetafilesAsPng = true;

            // Set text compression level
            pdfOptions.TextCompression = PdfTextCompression.Flate;

            // Define the PDF standard
            pdfOptions.Compliance = PdfCompliance.Pdf15;

            // Save the presentation as PDF
            presentation.Save("C:\\Users\\vivek.kumar2\\Downloads\\PowerPoint-to-PDF.pdf", SaveFormat.Pdf, pdfOptions);
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