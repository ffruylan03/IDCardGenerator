using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CitizenIdentification.Models;
using ZXing;
using System.IO;
using System.Drawing.Imaging;

namespace CitizenIdentification.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult CitizenID()
        {
            var model = new CitizenIDViewModel()
            {
                Name = "Juan Dela D. Cruz",
                Age = 35,
                Address = "Door 2 Buenavista Bldg., Mandaluyong City",
                VaccinationDate1 = DateTime.Today.ToShortDateString(),
            };

            var codeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE
            };
            var id = Guid.NewGuid().ToString();
            var code = codeWriter.Write(id);

            using (var memory = new MemoryStream())
            {
                // Store in Memory
                code.Save(memory, ImageFormat.Png);

                // Convert to Base64
                var toBase64 = Convert.ToBase64String(memory.ToArray());
                var toImgFile = $"data:image/png;base64,{toBase64}";
                model.QRCode = toImgFile;
            }
            return View(model);
        }
    }
}
