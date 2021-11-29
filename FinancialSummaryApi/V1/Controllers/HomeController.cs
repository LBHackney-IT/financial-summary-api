using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System.IO;

namespace FinancialSummaryApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/test")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class HomeController : BaseController
    {
        //To get content root path of the project
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HomeController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult ExportToPDF()
        {
            //Initialize HTML to PDF converter 
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

            WebKitConverterSettings settings = new WebKitConverterSettings();

            //Set WebKit pathQtBinariesWindows,QtBinariesLinux
            settings.WebKitPath = Path.Combine(_hostingEnvironment.ContentRootPath, "QtBinariesLinux");

            //Assign WebKit settings to HTML converter
            htmlConverter.ConverterSettings = settings;
            byte[] result;
            //Convert URL to PDF
            PdfDocument document = htmlConverter.Convert("http://www.google.com");
            using (var stream = new MemoryStream())
            {
                document.Save(stream);
                result = stream.ToArray();
            }

            //Save and close the PDF document 


            return File(result, System.Net.Mime.MediaTypeNames.Application.Pdf, "Sample.pdf");
        }
    }
}
