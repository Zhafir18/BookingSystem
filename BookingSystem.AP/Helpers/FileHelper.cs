namespace DanCoffee.Web.Customer.Helpers
{
    public class FileHelper
    {
        public static string UploadFile(string fileName, IFormFile formFile, string url)
        {
            var uploadDirectory = string.Format("Resource\\image\\{0}\\", url);
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), uploadDirectory);

            if (string.IsNullOrEmpty(fileName))
            {
                var uuid = Guid.NewGuid();
                fileName = string.Format("{0}.{1}", uuid.ToString(), GetFileExtension(formFile.FileName));
            }

            if (Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            else
            {
                Directory.CreateDirectory(uploadPath);
            }

            var filePath = Path.Combine(uploadPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }

            return fileName;
        }
        private static string GetFileExtension(string fileName)
        {
            var lastDotIndex = fileName.LastIndexOf('.');
            if (lastDotIndex != -1 && lastDotIndex != 0)
            {
                return fileName.Substring(lastDotIndex + 1);
            }
            else
            {
                return string.Empty;
            }
        }

        public static void DeleteFile(string fileName, string url)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var uploadDirectory =string.Format("Resource\\image\\{0}\\", url);
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), uploadDirectory);
                var filePath = Path.Combine(uploadPath, fileName);

                File.Delete(filePath);
            }
        }
    }
}
