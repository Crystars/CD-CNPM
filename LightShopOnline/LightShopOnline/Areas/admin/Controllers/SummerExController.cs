using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Areas.admin.Controllers
{
    [Area("admin")]
    public class SummerExController : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;

        public SummerExController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        //get index
        public async Task<ActionResult> Index()
        {
            return Redirect("/");
        }

        [HttpPost]
        public async Task<JsonResult> UploadFile(IFormFile uploadedFiles)
        {
            string returnImagePath = string.Empty;
            if (uploadedFiles.Length > 0)
            {
                var imageName = await SaveImage(uploadedFiles);
                returnImagePath = "/asset/img/" + imageName;
            }

            return Json(Convert.ToString(returnImagePath));
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string fileName;
            string Extension;
            string imageName;

            fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
            Extension = Path.GetExtension(imageFile.FileName);

            imageName = new String(fileName.Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yyyyMMddHHmmss") + Extension;
            var imagePath = Path.Combine(_appEnvironment.WebRootPath, "asset", "img", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }
    }
}
