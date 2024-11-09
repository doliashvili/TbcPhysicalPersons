using FluentValidation;
using Tbc.PhysicalPersonsDirectory.Domain.Enums;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetDetailedFilteredPagedData;

//აქ აღარ გავაკეთე ტექსტების თარგმნა(სატესტო დავალებისთვის საკმარისია მგონი მარტო ქომანდებში)
public class GetDetailedFilteredPagedDataQueryValidator : AbstractValidator<GetDetailedFilteredPagedDataQuery>
{
    public GetDetailedFilteredPagedDataQueryValidator()
    {
        // Validate FirstName - required, between 2 and 50 characters, only Georgian or Latin letters, but not both
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .Matches(@"^(?:[ა-ჰ]+|[A-Za-z]+)$").WithMessage("First name must contain only Georgian or Latin letters, but not both.")
            .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.")
            .When(x => !string.IsNullOrEmpty(x.FirstName));  // Only validate if not null or empty

        // Validate LastName - required, between 2 and 50 characters, only Georgian or Latin letters, but not both
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .Matches(@"^(?:[ა-ჰ]+|[A-Za-z]+)$").WithMessage("Last name must contain only Georgian or Latin letters, but not both.")
            .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.")
            .When(x => !string.IsNullOrEmpty(x.LastName));  // Only validate if not null or empty

        // Validate Gender - required, only "Female" or "Male"
        RuleFor(x => x.Gender)
            .Must(g => g == Gender.Female || g == Gender.Male).WithMessage("Gender must be either 'Female' or 'Male'.")
            .When(x => x.Gender.HasValue); // Only validate if Gender is not null

        // Validate PersonalNumber - required, exactly 11 digits
        RuleFor(x => x.PersonalNumber)
            .NotEmpty().WithMessage("Personal number is required.")
            .Matches(@"^\d{11}$").WithMessage("Personal number must be exactly 11 digits.")
            .When(x => !string.IsNullOrEmpty(x.PersonalNumber));  // Only validate if not null or empty

        // Validate BirthDate - required, must be at least 18 years old
        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("Birth date is required.")
            .Must(BeAtLeast18YearsOld).WithMessage("Person must be at least 18 years old.")
            .When(x => x.BirthDate.HasValue);  // Only validate if BirthDate is not null

        // Validate CityId - required (must exist in city reference table, this could be handled in service layer)
        RuleFor(x => x.CityId)
            .GreaterThan(0).WithMessage("City ID is required and must be valid.")
            .When(x => x.CityId.HasValue);  // Only validate if CityId is not null

        //!pageNumber.HasValue allows PageNumber to be null.
        //pageNumber.Value >= 0 ensures that if PageNumber has a value, it must be zero or positive.
        RuleFor(x => x.PageNumber)
            .Must(pageNumber => !pageNumber.HasValue || pageNumber.Value >= 0)
            .WithMessage("PageNumber must be either null or a positive number or zero.");
    }

    // Custom validator to ensure person is at least 18 years old
    private bool BeAtLeast18YearsOld(DateTime? birthDate)
    {
        if (birthDate == null) return false;

        var age = DateTime.Now.Year - birthDate.Value.Year;
        if (DateTime.Now.DayOfYear < birthDate.Value.DayOfYear)
            age--;
        return age >= 18;
    }
}