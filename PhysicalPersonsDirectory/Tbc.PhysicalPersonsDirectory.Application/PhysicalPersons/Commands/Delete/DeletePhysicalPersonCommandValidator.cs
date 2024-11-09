using FluentValidation;
using Microsoft.Extensions.Localization;
using Tbc.PhysicalPersonsDirectory.Application.Constants;
using Tbc.PhysicalPersonsDirectory.Application.Resources;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Delete
{
    public class DeletePhysicalPersonCommandValidator : AbstractValidator<DeletePhysicalPersonCommand>
    {
        public DeletePhysicalPersonCommandValidator(IStringLocalizer<ExceptionResources> textTranslator)
        {
            // Validate Id - required
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage(textTranslator[ValidationResourcesConstants.IdRequired]);
        }
    }
}