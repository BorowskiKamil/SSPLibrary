using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SSPLibrary.Infrastructure;

namespace SSPLibrary.Models
{
    public class SearchOptions<T> : IValidatableObject
    {
        public IEnumerable<SearchTerm> SearchTerms { get; set; }

        public void ParseQuery(string parameter)
        {
            var processor = new SearchOptionsProcessor<T>(SearchTerms);
            SearchTerms = processor.ParseAllTerms(parameter);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SearchTerms == null) yield break;

            var processor = new SearchOptionsProcessor<T>(SearchTerms);

            var validTerms = processor.GetValidTerms().Select(x => x.Name);
            var invalidTerms = SearchTerms.Select(x => x.Name).Except(validTerms, StringComparer.OrdinalIgnoreCase);

            foreach (var term in invalidTerms)
            {
                yield return new ValidationResult(
                    $"Invalid search term '{term}'.",
                    new[] { nameof(SearchTerms) });
            }
        }
    }
}
