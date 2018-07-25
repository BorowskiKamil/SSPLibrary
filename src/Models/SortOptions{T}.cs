using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SSPLibrary.Infrastructure;

namespace SSPLibrary.Models
{
    public class SortOptions<T> : IValidatableObject
    {
        public IEnumerable<SortTerm> SortTerms { get; set; }

        public void ParseQuery(string parameter)
        {
            if (parameter == null) return;

            var processor = new SortOptionsProcessor<T>(SortTerms);
            SortTerms = processor.ParseAllTerms(parameter);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var processor = new SortOptionsProcessor<T>(SortTerms);

            var validTerms = processor.GetValidTerms().Select(x => x.Name);

            var invalidTerms = SortTerms.Select(x => x.Name).Except(validTerms, StringComparer.OrdinalIgnoreCase);

            foreach (var term in invalidTerms)
            {
                yield return new ValidationResult(
                    $"Invalid sort term '{term}'.",
                    new[] { nameof(SortTerms) }
                );
            }
        }
    }
}
