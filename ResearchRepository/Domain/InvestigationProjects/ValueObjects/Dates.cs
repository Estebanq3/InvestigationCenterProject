using ResearchRepository.Domain.Core.ValueObjects;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Domain.InvestigationProjects.ValueObjects
{
    public class Dates : ValueObject
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }


        private Dates(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public static Validation<ValidationError, Dates> TryCreate(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                return new EndDateBeforeStartDate(endDate);
            else 
            {
                return new Dates(startDate,endDate);
            }

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return null;
        }

        public abstract record ValidationError;
        public record EndDateBeforeStartDate(DateTime endDate) : ValidationError;
        public record CodeTooBig(int MaxNumber) : ValidationError;
    }
}

