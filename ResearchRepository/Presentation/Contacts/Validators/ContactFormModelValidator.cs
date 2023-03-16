using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Presentation.Contacts.Models;

namespace ResearchRepository.Presentation.Contacts.Validators
{
    public class ContactFormModelValidator : AbstractValidator<ContactFormModel>
    {
        private int _nameMaxLength = 35;
        private int _messageMaxLength = 1000;
        public ContactFormModelValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(_nameMaxLength)
                .WithName("Nombre");

            RuleFor(x => x.Subject)
                .NotEmpty()
                .MaximumLength(35)
                .WithName("Asunto");

            RuleFor(x => x.Organization)
                .MaximumLength(100)
                .WithName("Organización");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress(EmailValidationMode.Net4xRegex)
                .WithName("Correo");

            RuleFor(x => x.Message)
                .NotEmpty()
                .MaximumLength(_messageMaxLength)
                .WithName("Mensaje"); 
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ContactFormModel>.CreateWithOptions((ContactFormModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
