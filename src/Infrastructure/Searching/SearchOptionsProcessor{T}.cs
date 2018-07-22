using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SSPLibrary.Infrastructure
{
	public class SearchOptionsProcessor<T>
	{
	
		private IEnumerable<SearchTerm> _searchTerms;

        public SearchOptionsProcessor(IEnumerable<SearchTerm> searchTerms)
        {
			_searchTerms = searchTerms;
        }

		public IEnumerable<SearchTerm> ParseAllTerms(string searchQuery)
		{
			_searchTerms = GetAllTerms(searchQuery);
            return _searchTerms;
		}

		private IEnumerable<SearchTerm> GetAllTerms(string searchQuery)
        {
            if (searchQuery == null) yield break;

			string[] order = searchQuery.Split(',');
            var arrayQuery = order.Select(x => 
            {
                return x.Trim();
            }).ToArray();

			var operators = SearchOperator.GetAllOperators();

            foreach (var term in arrayQuery)
            {
                if (string.IsNullOrEmpty(term)) continue;

				string separator = FindMatchingOperator(term, operators.Values);

				if (separator == null)
				{
					yield return new SearchTerm
					{
						ValidSyntax = false,
                        Name = term
					};

					continue;
				}

				var termSplited = term.Split(separator.ToCharArray());

				if (termSplited.Length < 2)
                {
                    yield return new SearchTerm
                    {
                        ValidSyntax = false,
                        Name = termSplited[0]
                    };

                    continue;
                }

				yield return new SearchTerm
                {
                    ValidSyntax = true,
                    Name = termSplited[0],
                    Operator = separator,
                    Value = string.Join(separator, termSplited.Skip(1))
                };
            }
        }

		public IEnumerable<SearchTerm> GetValidTerms()
        {
            var queryTerms = _searchTerms
                .Where(t => t.ValidSyntax)
                .ToArray();

            if (!queryTerms.Any()) yield break;

            var declaredTerms = GetTermsFromModel();

            foreach (var term in queryTerms)
            {
                var declaredTerm = declaredTerms
                    .SingleOrDefault(x => x.Name.Equals(term.Name, StringComparison.OrdinalIgnoreCase));
                if (declaredTerm == null) continue;

                yield return new SearchTerm
                {
                    ValidSyntax = term.ValidSyntax,
                    Name = declaredTerm.Name,
                    Operator = term.Operator,
                    Value = term.Value,
                    ExpressionProvider = declaredTerm.ExpressionProvider
                };

            }
        }

		private static IEnumerable<SearchTerm> GetTermsFromModel()
            => typeof(T).GetTypeInfo()
                .DeclaredProperties
                .Where(p => p.GetCustomAttributes<SearchableAttribute>().Any())
                .Select(p => new SearchTerm 
                { 
                    Name = p.Name,
                    ExpressionProvider = p.GetCustomAttribute<SearchableAttribute>().ExpressionProvider
                });


		private string FindMatchingOperator(string term, IEnumerable<string> operators)
		{
			foreach (var op in operators)
			{
				if (!term.Contains(op)) continue;

				var termSplited = term.Split(op.ToCharArray());

				if (termSplited.Count() < 2) continue;

				if (!Regex.IsMatch(termSplited[0], @"^[a-zA-Z_]\w*(\.[a-zA-Z_]\w*)*$")) continue;

				return op;
			}

			return null;
		}


	}
}
