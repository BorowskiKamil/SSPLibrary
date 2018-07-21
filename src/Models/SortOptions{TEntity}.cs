using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SSPLibrary.Infrastructure;

namespace SSPLibrary.Models
{
    public class SortOptions<TEntity> : IValidatableObject
    {

        public string[] OrderBy { get; set; }

        public IEnumerable<ValidationResult> ParseQuery(string parameter, ValidationContext validationContext)
        {
            string[] order = parameter.Split(',');
            OrderBy = order.Select(x => 
            {
                return x.Trim();
            }).ToArray();

            return Validate(validationContext);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var processor = new SortOptionsProcessor<TEntity>(OrderBy);

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

        // public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        // {
        //     var processor = new SortOptionsProcessor<TEntity>(OrderBy);
        //     return processor.Apply(query);
        // }
    }
}
