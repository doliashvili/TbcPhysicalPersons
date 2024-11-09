using Tbc.PhysicalPersonsDirectory.Application.Services;

namespace Tbc.PhysicalPersonsDirectory.Infrastructure.Implements.Services
{
    public class ImageStorageService : IImageStorageService
    {
        private readonly string _storageBaseUrl;
        private readonly string _storageDirectory;

        public ImageStorageService()
        {
            _storageBaseUrl = "file:///";
            // Get the base directory of the current application domain
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _storageDirectory = Path.Combine(baseDirectory, "TbcImages");
        }

        public Task DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                throw new ArgumentException("Image URL cannot be null or empty.", nameof(imageUrl));

            // Extract the file path from the URL
            Uri uri;
            if (!Uri.TryCreate(imageUrl, UriKind.Absolute, out uri))
            {
                throw new ArgumentException("Invalid image URL format.", nameof(imageUrl));
            }

            var filePath = uri.LocalPath.Replace('/', Path.DirectorySeparatorChar);

            // Check if the file exists and delete it
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.CompletedTask;
        }

        public async Task<string> UploadImageAsync(Stream imageStream, string fileName)
        {
            if (imageStream == null)
                throw new ArgumentNullException(nameof(imageStream));

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));

            // Generate a unique file name to prevent conflicts
            var uniqueFileName = Guid.NewGuid().ToString("N") + Path.GetExtension(fileName);
            var filePath = Path.Combine(_storageDirectory, uniqueFileName);

            // Ensure the storage directory exists
            if (!Directory.Exists(_storageDirectory))
            {
                Directory.CreateDirectory(_storageDirectory);
            }

            // Save the image stream to the file
            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageStream.CopyToAsync(fileStream);
            }

            // Construct the URL based on the base URL and relative file path
            var relativeFilePath = filePath.Replace(Path.DirectorySeparatorChar, '/');
            var imageUrl = $"{_storageBaseUrl.TrimEnd('/')}/{relativeFilePath}";

            // Return the URL
            return imageUrl;
        }
    }
}