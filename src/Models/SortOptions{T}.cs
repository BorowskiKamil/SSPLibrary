using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SSPLibrary.Infrastructure;

namespace SSPLibrary.Models
{
    public class SortOptions<T> : IValidatableObject
    {

        public string[] OrderBy { get; set; }

        public void ParseQuery(string parameter)
        {
            if (parameter == null) return;

            string[] order = parameter.Split(',');
            OrderBy = order.Select(x => 
            {
                return x.Trim();
            }).ToArray();

            // return Validate(validationContext);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var processor = new SortOptionsProcessor<T>(OrderBy);

            var validTerms = processor.GetValidTerms().Select(x => x.Name);

            var invalidTerms = processor.GetAllTerms().Select(x => x.Name).Except(validTerms, StringComparer.OrdinalIgnoreCase);

            foreach (var term in invalidTerms)
            {
                yield return new ValidationResult(
                    $"Invalid sort term '{term}'.",
                    new[] { nameof(OrderBy) }
                );
            }
        }
    }
}
