using System.IO;
using System.Threading.Tasks;

namespace ePizzaHub.WebUI.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> UploadFileToBlobAsync(string strFileName, Stream content, string contentType);
        void DeleteBlobData(string fileUrl);
    }
}
