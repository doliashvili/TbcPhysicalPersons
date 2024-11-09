using FluentValidation;
using Microsoft.Extensions.Localization;
using Tbc.PhysicalPersonsDirectory.Application.Constants;
using Tbc.PhysicalPersonsDirectory.Application.Resources;
using Tbc.PhysicalPersonsDirectory.Domain.Enums;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Create
{
    public class CreatePhysicalPersonCommandValidator : AbstractValidator<CreatePhysicalPersonCommand>
    {
        public CreatePhysicalPersonCommandValidator(IStringLocalizer<ExceptionResources> textTranslator)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(textTranslator[ValidationResourcesConstants.FirstNameRequired])
                .Matches(@"^(?:[ა-ჰ]+|[A-Za-z]+)$").WithMessage(textTranslator[ValidationResourcesConstants.FirstNameFormat])
                .Length(2, 50).WithMessage(textTranslator[ValidationResourcesConstants.FirstNameLength]);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(textTranslator[ValidationResourcesConstants.LastNameRequired])
                .Matches(@"^(?:[ა-ჰ]+|[A-Za-z]+)$").WithMessage(textTranslator[ValidationResourcesConstants.LastNameFormat])
                .Length(2, 50).WithMessage(textTranslator[ValidationResourcesConstants.LastNameLength]);

            RuleFor(x => x.Gender)
                .Must(g => g == Gender.Female || g == Gender.Male).WithMessage(textTranslator[ValidationResourcesConstants.GenderInvalid]);

            RuleFor(x => x.PersonalNumber)
                .NotEmpty().WithMessage(textTranslator[ValidationResourcesConstants.PersonalNumberRequired])
                .Matches(@"^\d{11}$").WithMessage(textTranslator[ValidationResourcesConstants.PersonalNumberFormat]);

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage(textTranslator[ValidationResourcesConstants.BirthDateRequired])
                .Must(BeAtLeast18YearsOld).WithMessage(textTranslator[ValidationResourcesConstants.AgeInvalid]);

            RuleFor(x => x.CityId)
                .GreaterThan(0).WithMessage(textTranslator[ValidationResourcesConstants.CityIdRequired]);

            RuleForEach(x => x.PhoneNumbers)
                .ChildRules(phone =>
                {
                    phone.RuleFor(p => p.Type)
                        .IsInEnum().WithMessage(textTranslator[ValidationResourcesConstants.PhoneTypeInvalid]);

                    phone.RuleFor(p => p.Number)
                        .NotEmpty().WithMessage(textTranslator[ValidationResourcesConstants.PhoneNumberRequired])
                        .Length(4, 50).WithMessage(textTranslator[ValidationResourcesConstants.PhoneNumberLength])
                        .Matches(@"^\+?\d+$").WithMessage(textTranslator[ValidationResourcesConstants.PhoneNumberFormat]);
                });
        }

        // Custom validator to ensure person is at least 18 years old
        private bool BeAtLeast18YearsOld(DateTime birthDate)
        {
            var age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.DayOfYear < birthDate.DayOfYear)
                age--;
            return age >= 18;
        }
    }
}