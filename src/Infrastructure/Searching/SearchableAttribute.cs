using System;

namespace SSPLibrary.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SearchableAttribute : Attribute
    {

        public ISearchExpressionProvider ExpressionProvider { get; set; }
            // = new DefaultSearchExpressionProvider();


    }
}
