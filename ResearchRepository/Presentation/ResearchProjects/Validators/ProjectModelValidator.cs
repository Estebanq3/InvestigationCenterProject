using FluentValidation;
using ResearchRepository.Presentation.ResearchProjects.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.InvestigationProjects.ValueObjects;

namespace ResearchRepository.Presentation.ResearchProjects.Validators
{
    public class ProjectModelValidator : AbstractValidator<ProjectModel>
    {
        private int nameMaxLength = 200;
        private int descriptionMaxLength = 8000;
        public ProjectModelValidator()
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

                context.AddFailure(nameof(ProjectModel.Name), errorMessage);
            });

            //check description
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

                context.AddFailure(nameof(ProjectModel.Description), errorMessage);
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

                context.AddFailure(nameof(ProjectModel.Summary), errorMessage);
            });

            //check date
            RuleFor(p => p)
               .Custom((model, context) => {
                   var result = Dates.TryCreate(model.StartDate,model.EndDate);
                   if (!result.IsFail)
                       return;

                   var error = result.Fail();
                   var errorMessage = error switch
                   {
                       Dates.EndDateBeforeStartDate => $"Please choose a end date older than star date.",
                       _ => throw new ArgumentOutOfRangeException(nameof(error))
                   };

                   context.AddFailure(nameof(ProjectModel.EndDate), errorMessage);
                   context.AddFailure(nameof(ProjectModel.StartDate), errorMessage);
               });

        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ProjectModel>.CreateWithOptions((ProjectModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
