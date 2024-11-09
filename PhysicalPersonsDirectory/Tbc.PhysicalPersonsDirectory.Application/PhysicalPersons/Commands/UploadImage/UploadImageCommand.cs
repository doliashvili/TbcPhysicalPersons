using MediatR;
using Microsoft.AspNetCore.Http;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.UploadImage.Model;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.UploadImage
{
    public class UploadImageCommand : IRequest<UploadImageResponse>
    {
        public int PersonId { get; set; }
        public IFormFile Image { get; set; }
    }
}