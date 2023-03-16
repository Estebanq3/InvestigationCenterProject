using FluentValidation;
using ResearchRepository.Presentation.ResearchTheses.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Theses;

namespace ResearchRepository.Presentation.ResearchTheses.Validators
{
    public class ThesisModelValidator : AbstractValidator<ThesisModel>
    {
        private int nameMaxLength = 300;
        private int descriptionMaxLength = 8000;
        public ThesisModelValidator()
        {
            // check name
            RuleFor(p => p.Name)
            .Custom((name, context) =>
            {
                var result = RequiredString.TryCreate(name, nameMaxLength);
                if (!result.IsFail)
                    return;

                var error = result.Fail();
                var errorMessage = error switch
                {
                    RequiredString.IsNullOrWhitespace _ => "Please type the name",
                    RequiredString.TooLong tooLong => $"Please type less than {nameMaxLength} characters.",
                    _ => throw new ArgumentOutOfRangeException(nameof(error))
                };

                context.AddFailure(nameof(ThesisModel.Name), errorMessage);
            });

            //check summary
            RuleFor(p => p.Summary)
            .Custom((summary, context) =>
            {
                var result = RequiredString.TryCreate(summary, descriptionMaxLength);
                if (!result.IsFail)
                    return;

                var error = result.Fail();
                var errorMessage = error switch
                {
                    RequiredString.IsNullOrWhitespace _ => "Please type the news's summary.",
                    RequiredString.TooLong tooLong => $"Please type less than {descriptionMaxLength} characters.",
                    _ => throw new ArgumentOutOfRangeException(nameof(error))
                };

                context.AddFailure(nameof(ThesisModel.Summary), errorMessage);
            });

        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ThesisModel>.CreateWithOptions((ThesisModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}