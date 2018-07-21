using System;

namespace SSPLibrary.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SortableAttribute : Attribute
    {

        public bool Default { get; set; }

        public bool WhenDefaultIsDescending { get; set; }

    }
}
