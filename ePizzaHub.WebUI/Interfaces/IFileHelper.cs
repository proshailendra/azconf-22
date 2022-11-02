using Microsoft.AspNetCore.Http;

namespace ePizzaHub.WebUI.Interfaces
{
    public interface IFileHelper
    {
        void DeleteFile(string imageUrl);

        string UploadFile(IFormFile file);
    }
}