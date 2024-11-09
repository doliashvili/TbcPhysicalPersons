using FluentValidation;
using Microsoft.Extensions.Localization;
using Tbc.PhysicalPersonsDirectory.Application.Constants;
using Tbc.PhysicalPersonsDirectory.Application.Resources;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetByIdIncludedData;

public class GetByIdIncludedDataQueryValidator : AbstractValidator<GetByIdIncludedDataQuery>
{
    public GetByIdIncludedDataQueryValidator(IStringLocalizer<ExceptionResources> textTranslator)
    {
        // Validate Id - required
        RuleFor(x => x.PhysicalPersonId)
            .GreaterThan(0).WithMessage(textTranslator[ValidationResourcesConstants.IdRequired]);
    }
}