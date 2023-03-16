using FluentValidation;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Presentation.Contacts.Models;

namespace ResearchRepository.Presentation.Contacts.Validators
{
    public class ContactModelValidator : AbstractValidator<ContactModel>
    {
        private int titleMaxLength = 200;
        private int descriptionMaxLength = 8000;
        public ContactModelValidator()
        {
            // check name
            RuleFor(p => p.Name)
            .Custom((title, context) =>
            {
                var result = RequiredString.TryCreate(title, titleMaxLength);
                if (!result.IsFail)
                    return;

                var error = result.Fail();
                var errorMessage = error switch
                {
                    RequiredString.IsNullOrWhitespace _ => "Please type the contact's name.",
                    RequiredString.TooLong tooLong => $"Please type less than {titleMaxLength} characters.",
                    _ => throw new ArgumentOutOfRangeException(nameof(error))
                };

                context.AddFailure(nameof(ContactModel.Name), errorMessage);
            });

            RuleFor(p => p.Email)
            .Custom((description, context) =>
            {
                var result = RequiredString.TryCreate(description, descriptionMaxLength);
                if (!result.IsFail)
                    return;

                var error = result.Fail();
                var errorMessage = error switch
                {
                    RequiredString.IsNullOrWhitespace _ => "Please type the contact's email.",
                    RequiredString.TooLong tooLong => $"Please type less than {descriptionMaxLength} characters.",
                    _ => throw new ArgumentOutOfRangeException(nameof(error))
                };

                context.AddFailure(nameof(ContactModel.Email), errorMessage);
            });
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ContactModel>.CreateWithOptions((ContactModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
