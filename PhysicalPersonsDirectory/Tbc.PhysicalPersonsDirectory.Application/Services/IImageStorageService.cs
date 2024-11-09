namespace Tbc.PhysicalPersonsDirectory.Application.Services
{
    public interface IImageStorageService
    {
        Task<string> UploadImageAsync(Stream imageStream, string fileName);

        Task DeleteImageAsync(string imageUrl);
    }
}