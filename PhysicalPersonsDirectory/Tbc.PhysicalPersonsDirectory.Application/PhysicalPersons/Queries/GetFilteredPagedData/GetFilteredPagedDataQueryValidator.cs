using FluentValidation;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetFilteredPagedData;

//აქ აღარ გავაკეთე ტექსტების თარგმნა(სატესტო დავალებისთვის საკმარისია მგონი მარტო ქომანდებში)
public class GetFilteredPagedDataQueryValidator : AbstractValidator<GetFilteredPagedDataQuery>
{
    public GetFilteredPagedDataQueryValidator()
    {
        RuleFor(x => x.SearchQuery)
            .NotEmpty().WithMessage("SearchQuery is required");

        //!pageNumber.HasValue allows PageNumber to be null.
        //pageNumber.Value >= 0 ensures that if PageNumber has a value, it must be zero or positive.
        RuleFor(x => x.PageNumber)
            .Must(pageNumber => !pageNumber.HasValue || pageNumber.Value >= 0)
            .WithMessage("PageNumber must be either null or a positive number or zero.");
    }
}