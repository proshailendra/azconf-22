using ePizzaHub.WebUI.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ePizzaHub.WebUI.Helpers
{
    public class FileHelper : IFileHelper
    {
        private IWebHostEnvironment _env;

        public FileHelper(IWebHostEnvironment env)
        {
            _env = env;
        }

        private string GenerateFileName(string fileName)
        {
            string[] strName = fileName.Split('.');
            string strFileName = DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") + "." + strName[strName.Length - 1];
            return strFileName;
        }

        public void DeleteFile(string imageUrl)
        {
            //delete existing file
            if (File.Exists(_env.WebRootPath + imageUrl))
            {
                File.Delete(_env.WebRootPath + imageUrl);
            }
        }

        public string UploadFile(IFormFile file)
        {
            var uploads = Path.Combine(_env.WebRootPath, "images");
            bool exists = Directory.Exists(uploads);
            if (!exists)
                Directory.CreateDirectory(uploads);

            //saving file
            var fileName = GenerateFileName(file.FileName);
            var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);
            file.CopyToAsync(fileStream);

            return "/images/" + fileName;
        }
    }
}