﻿using System;
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

			var operators = SearchOperator.GetAllOperators().OrderByDescending(x => x.Value.Length).Select(x => x.Value).ToArray();

            foreach (var term in arrayQuery)
            {
                if (string.IsNullOrEmpty(term)) continue;

				string separator = FindMatchingOperator(term, operators);

				if (separator == null)
				{
					yield return new SearchTerm
					{
						ValidSyntax = false,
                        Name = term
					};

					continue;
				}

				var termSplited = term.Split(new[] { separator }, StringSplitOptions.None);

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
                    Name = termSplited[0].Trim(),
                    Operator = separator,
                    Value = string.Join(separator, termSplited.Skip(1)).Trim()
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

        private static ISearchExpressionProvider GetDefaultExpressionProvider(Type type, ISearchExpressionProvider eProvider)
        {
            if (eProvider != null)
                return eProvider;

            if (type.IsNumericType())
                return new ComparableSearchExpressionProvider();

            if (type == typeof(DateTime))
                return new DateTimeSearchExpressionProvider();

            if (type == typeof(DateTimeOffset))
                return new DateTimeOffsetSearchExpressionProvider();

            if (type == typeof(string))
                return new StringSearchExpressionProvider();

            return new DefaultSearchExpressionProvider();
        }

		private static IEnumerable<SearchTerm> GetTermsFromModel()
            => typeof(T).GetTypeInfo()
                .DeclaredProperties
                .Where(p => p.GetCustomAttributes<SearchableAttribute>().Any())
                .Select(p => new SearchTerm 
                {
                    Name = p.Name,
                    ExpressionProvider = GetDefaultExpressionProvider(p.PropertyType, p.GetCustomAttribute<SearchableAttribute>().ExpressionProvider),
                });


		private string FindMatchingOperator(string term, IEnumerable<string> operators)
		{
			foreach (var op in operators)
			{
				if (!term.Contains(op)) continue;

				var termSplited = term.Split(new[] { op }, StringSplitOptions.None);

				if (termSplited.Count() < 2) continue;

				if (!Regex.IsMatch(termSplited[0], @"^[a-zA-Z_]\w*(\.[a-zA-Z_]\w*)*$")) continue;

				return op;
			}

			return null;
		}

        public IQueryable<T> Apply(IQueryable<T> query)
        {
            var terms = GetValidTerms().ToArray();
            if (!terms.Any()) return query;

            var modifiedQuery = query;

            foreach (var term in terms)
            {
                var propertyInfo = ExpressionHelper
                    .GetPropertyInfo<T>(term.Name);
                var obj = ExpressionHelper.Parameter<T>();

                var left = ExpressionHelper.GetPropertyExpression(obj, propertyInfo);
                var right = term.ExpressionProvider.GetValue(term.Value, propertyInfo.PropertyType);

                var comparisionExpression = term.ExpressionProvider
                                                .GetComparison(left, term.Operator, right);

                var lambdaExpression = ExpressionHelper.GetLambda<T, bool>(obj, comparisionExpression);

                modifiedQuery = ExpressionHelper.CallWhere(modifiedQuery, lambdaExpression);
            }

            return modifiedQuery;
        }


	}
}
