using System;

namespace SSPLibrary.Models
{
    public class PagingOptions
    {
        public int DefaultLimit { get; set; } = 25;

        public int MaxLimit { get; set; } = 100;

    }
}