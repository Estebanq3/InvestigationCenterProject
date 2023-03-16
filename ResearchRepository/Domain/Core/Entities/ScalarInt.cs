using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Domain.Core.Entities
{
    /// <summary>
    /// This class is a helper to get the result of StoredProcedure (SP) that returns a table with
    /// the column Value of type int. This is useful in SP that uses the agreggate function COUNT
    /// </summary>
    /// Author: Tyron Fonseca
    public partial class ScalarInt
    {
        public int Value { get; set; }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
