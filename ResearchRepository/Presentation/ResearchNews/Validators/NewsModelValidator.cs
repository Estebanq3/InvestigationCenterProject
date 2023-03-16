using FluentValidation;
using ResearchRepository.Presentation.ResearchNews.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Helpers;

namespace ResearchRepository.Presentation.ResearchNews.Validators
{
    public class NewsModelValidator : AbstractValidator<NewsModel>
    {
        private int titleMaxLength = 200;
        private int descriptionMaxLength = 8000;
        public NewsModelValidator()
        {
            // check name
            RuleFor(p => p.Title)
            .Custom((title, context) =>
            {
                var result = RequiredString.TryCreate(title, titleMaxLength);
                if (!result.IsFail)
                    return;

                var error = result.Fail();
                var errorMessage = error switch
                {
                    RequiredString.IsNullOrWhitespace _ => "Please type the news's title.",
                    RequiredString.TooLong tooLong => $"Please type less than {titleMaxLength} characters.",
                    _ => throw new ArgumentOutOfRangeException(nameof(error))
                };

                context.AddFailure(nameof(NewsModel.Title), errorMessage);
            });

            RuleFor(p => p.Description)
            .Custom((description, context) =>
            {
                var result = RequiredString.TryCreate(description, descriptionMaxLength);
                if (!result.IsFail)
                    return;

                var error = result.Fail();
                var errorMessage = error switch
                {
                    RequiredString.IsNullOrWhitespace _ => "Please type the news's description.",
                    RequiredString.TooLong tooLong => $"Please type less than {descriptionMaxLength} characters.",
                    _ => throw new ArgumentOutOfRangeException(nameof(error))
                };

                context.AddFailure(nameof(NewsModel.Description), errorMessage);
            });
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<NewsModel>.CreateWithOptions((NewsModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
