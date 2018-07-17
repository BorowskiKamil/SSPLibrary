using System;

namespace SSPLibrary.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PaginableAttributeOld : Attribute
    {
        public bool Default { get; set; }

    }
}
