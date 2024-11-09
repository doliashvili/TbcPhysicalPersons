using FluentValidation;
using Microsoft.Extensions.Localization;
using Tbc.PhysicalPersonsDirectory.Application.Constants;
using Tbc.PhysicalPersonsDirectory.Application.Resources;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.CreateRelation
{
    public class CreateRelationCommandValidator : AbstractValidator<CreateRelationCommand>
    {
        public CreateRelationCommandValidator(IStringLocalizer<ExceptionResources> textTranslator)
        {
            RuleFor(x => x.MainPersonId)
                .GreaterThan(0).WithMessage(textTranslator[ValidationResourcesConstants.IdRequired]);

            RuleFor(x => x.RelationPersonId)
                .GreaterThan(0).WithMessage(textTranslator[ValidationResourcesConstants.IdRequired]);
        }
    }
}