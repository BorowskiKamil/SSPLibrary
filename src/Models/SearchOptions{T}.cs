using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SSPLibrary.Infrastructure;

namespace SSPLibrary.Models
{
    public class SearchOptions<T> : IValidatableObject
    {
        public string[] Search { get; set; }

        public IEnumerable<ValidationResult> ParseQuery(string parameter, ValidationContext validationContext)
        {
            string[] order = parameter.Split(',');
            Search = order.Select(x => 
            {
                return x.Trim();
            }).ToArray();

            return Validate(validationContext);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var processor = new SearchOptionsProcessor<T>();

            return null;
        }
    }
}
