using FluentValidation;
using Microsoft.Extensions.Localization;
using Tbc.PhysicalPersonsDirectory.Application.Constants;
using Tbc.PhysicalPersonsDirectory.Application.Resources;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.DeleteRelation
{
    public class DeleteRelationCommandValidator : AbstractValidator<DeleteRelationCommand>
    {
        public DeleteRelationCommandValidator(IStringLocalizer<ExceptionResources> textTranslator)
        {
            RuleFor(x => x.MainPersonId)
                .GreaterThan(0).WithMessage(textTranslator[ValidationResourcesConstants.IdRequired]);

            RuleFor(x => x.RelationPersonId)
                .GreaterThan(0).WithMessage(textTranslator[ValidationResourcesConstants.IdRequired]);
        }
    }
}