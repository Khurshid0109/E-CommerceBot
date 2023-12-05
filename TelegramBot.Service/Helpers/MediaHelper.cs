using Microsoft.AspNetCore.Http;

namespace TelegramBot.Service.Helpers;
public class MediaHelper
{
    public static async Task<string> UploadFile(IFormFile file)
    {
        string uniqueFileName = "";
        if (file != null && file.Length > 0)
        {
            string uploadsFolder = Path.Combine(WebHostEnvironment.WebRootPath, "Images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string imageFilePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(imageFilePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        return uniqueFileName;
    }
}
