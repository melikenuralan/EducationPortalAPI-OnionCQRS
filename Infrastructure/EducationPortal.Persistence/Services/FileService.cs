using EducationPortal.Application.Abstractions.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
namespace EducationPortal.Persistence.Services
{
    public class FileService : IFileService
    {
        readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            try
            {
                var contentPath = _environment.ContentRootPath;
                var path = Path.Combine(contentPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                    return new Tuple<int, string>(0, msg);
                }
                string uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, newFileName);
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, "Error has occured");
            }
        }
        public async Task DeleteImage(string imageFileName)
        {
            var contentPath = _environment.ContentRootPath;
            var path = Path.Combine(contentPath, $"Uploads", imageFileName);
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
