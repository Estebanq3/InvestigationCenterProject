using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ResearchRepository.Presentation.ResearchAreas.Models;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.ResearchAreas.Entities;

namespace ResearchRepository.Presentation.ResearchAreas.Validators
{
    public class ResearchAreaModelValidator : AbstractValidator<ResearchAreaModel>
    {
        private readonly IEnumerable<ResearchArea> _areas;
        private readonly string _originalName;

        public ResearchAreaModelValidator(IEnumerable<ResearchArea> areas, string name)
        {
            _areas = areas;
            _originalName = name;

            RuleFor(c => c.Name)
            .Custom((name, context) =>
            {
                var result = RequiredString.TryCreate(name,100);
                if (!result.IsFail)
                    return;

                var error = result.Fail();
                var errorMessage = error switch
                {
                    RequiredString.IsNullOrWhitespace _ => "Ingrese un nombre.",
                    RequiredString.TooLong tooLong => $"Escriba menos de {tooLong.MaxLength} carácteres.",
                    RequiredString.SQLInjection sql => $"{sql.Message}",
                    _ => throw new ArgumentOutOfRangeException(nameof(error))
                };

                context.AddFailure(nameof(ResearchAreaModel.Name), errorMessage);
            });

            RuleFor(c => c.Name).Must(c => UniqueName(c)).WithMessage("Nombre de área ya registrado");

            RuleFor(c => c.Description)
                .MaximumLength(8000).WithMessage($"Escriba menos de 8000 carácteres");

            When(c => c.isSubarea == true, () =>
            {
                RuleFor(c => c.ResearchAreas)
                    .NotNull().WithMessage($"Es necesario asociar la subárea a un área")
                    .Must(a => a.Any()).WithMessage($"Es necesario asociar la subárea a un área");
            });
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ResearchAreaModel>.CreateWithOptions((ResearchAreaModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };

        /// <summary>
        /// Checks if model name already exists
        /// </summary>
        /// <param name="Name"></param>
        /// <returns>boolean</returns>
        private bool UniqueName(string Name)
        {
            if (Name != null)
            {
                string name = Name.ToLower();

                if (_originalName != null && name.Equals(_originalName.ToLower()))
                    return true;

                foreach (var a in _areas)
                {
                    if (name.Equals(a.Name.ToString().ToLower()))
                        return false;

                    foreach (var s in a.ResearchSubAreas)
                    {
                        if (name.Equals(s.Name.ToString().ToLower()))
                            return false;
                    }
                }
            }
            return true;
        }
    }
}
