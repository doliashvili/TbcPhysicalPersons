using FluentValidation;
using Microsoft.Extensions.Options;
using Tbc.PhysicalPersonsDirectory.Application.Options;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.UploadImage
{
    public class UploadImageCommandValidator : AbstractValidator<UploadImageCommand>
    {
        private readonly ImagesOptions _imagesOptions;

        public UploadImageCommandValidator(IOptions<ImagesOptions> options)
        {
            _imagesOptions = options.Value;

            RuleFor(x => x.Image)
                .Must(file => CheckImageType(file.FileName))  // Pass the correct argument to the method
                .WithMessage($"Invalid image format. Allowed formats: {string.Join(", ", _imagesOptions.AllowedImageExtensions)}.");
        }

        private bool CheckImageType(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            var extension = Path.GetExtension(fileName).ToLower();
            return _imagesOptions.AllowedImageExtensions.Contains(extension);
        }
    }
}