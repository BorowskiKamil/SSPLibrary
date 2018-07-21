using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace SSPLibrary.Infrastructure
{
    public class SortOptionsProcessor<T>
    {

        private readonly string[] _orderBy;

        public SortOptionsProcessor(string[] orderBy)
        {
            _orderBy = orderBy;    
        }

        public IEnumerable<SortTerm> GetAllTerms()
        {
            if (_orderBy == null) yield break;

            foreach (var term in _orderBy)
            {
                if (string.IsNullOrEmpty(term)) continue;

                if (term.ElementAt(0) == '-')
                {
                    yield return new SortTerm
                    {
                        Name = term.Substring(1),
                        Descending = true,
                    };
                }
                else
                {
                    yield return new SortTerm
                    {
                        Name = term,
                    };
                }
            }
        }

        public IEnumerable<SortTerm> GetValidTerms()
        {
            var queryTerms = GetAllTerms().ToArray();
            if (!queryTerms.Any()) yield break;

            var declaredTerms = GetTermsFromModel();

            foreach (var term in queryTerms)
            {
                var declaredTerm = declaredTerms
                    .SingleOrDefault(x => x.Name.Equals(term.Name, StringComparison.OrdinalIgnoreCase));
                if (declaredTerm == null) continue;

                yield return new SortTerm
                {
                    Name = declaredTerm.Name,
                    Descending = term.Descending,
                    Default = declaredTerm.Default
                };
            }

        }

        public IQueryable<T> Apply(IQueryable<T> query)
        {
            var terms = GetValidTerms().ToArray();

            if (!terms.Any())
            {
                terms = GetTermsFromModel()
                            .Where(t => t.Default)
                            .Select(t => 
                            {
                                t.Descending = t.WhenDefaultIsDescending;
                                return t;
                            })
                            .ToArray();
            }

            if (!terms.Any()) return query;

            var modifiedQuery = query;
            var useThenBy = false;

            foreach (var term in terms)
            {
                var propertyInfo = ExpressionHelper.GetPropertyInfo<T>(term.Name);

                var obj = ExpressionHelper.Parameter<T>();

                var key = ExpressionHelper.GetPropertyExpression(obj, propertyInfo);
                var keySelector = ExpressionHelper.GetLambda(typeof(T), propertyInfo.PropertyType, obj, key);

                modifiedQuery = ExpressionHelper.CallOrderByOrThenBy(modifiedQuery, useThenBy, term.Descending, propertyInfo.PropertyType, keySelector);

                useThenBy = true;
            }

            return modifiedQuery;
        }

        private static IEnumerable<SortTerm> GetTermsFromModel()
            => typeof(T).GetTypeInfo()
                        .DeclaredProperties
                        .Where(p => p.GetCustomAttributes<SortableAttribute>().Any())
                        .Select(p => new SortTerm 
                        { 
                            Name = p.Name,
                            Default = p.GetCustomAttribute<SortableAttribute>().Default,
                            WhenDefaultIsDescending = p.GetCustomAttribute<SortableAttribute>().WhenDefaultIsDescending
                        });
    }
}
