using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tbc.PhysicalPersonsDirectory.Application.Exceptions;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.UploadImage.Model;
using Tbc.PhysicalPersonsDirectory.Application.Services;
using Tbc.PhysicalPersonsDirectory.Domain.Entities;
using Tbc.PhysicalPersonsDirectory.Domain.Repositories;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.UploadImage
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, UploadImageResponse>
    {
        private readonly IImageStorageService _imageStorageService;
        private readonly IRepository<PhysicalPersonEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UploadImageCommandHandler> _logger;

        public UploadImageCommandHandler(IImageStorageService imageStorageService,
             IRepository<PhysicalPersonEntity> repository,
             IUnitOfWork unitOfWork,
             ILogger<UploadImageCommandHandler> logger)
        {
            _imageStorageService = imageStorageService;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<UploadImageResponse> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start upload image");

            var personEntity = await _repository.GetByIdIncludedDataAsync(request.PersonId, query => query.Include(x => x.PhoneNumbers));

            if (personEntity is null)
            {
                throw new ObjectNotFoundException($"Physical person not found with this id: {request.PersonId}");
            }

            var image = request.Image;

            var stream = image.OpenReadStream();
            var fileName = image.FileName;

            var path = await _imageStorageService.UploadImageAsync(stream, fileName);

            try
            {
                personEntity.PicturePath = path;
                await _repository.UpdateAsync(personEntity, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                //if some errors throws we need delete image
                await _imageStorageService.DeleteImageAsync(path);
                throw;
            }

            _logger.LogInformation("Finish upload image with path: '{path}'", path);

            return new UploadImageResponse { Path = path };
        }
    }
}