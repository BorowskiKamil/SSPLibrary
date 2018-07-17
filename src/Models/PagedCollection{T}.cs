﻿using System;

namespace SSPLibrary.Models
{
    public class PagedCollection<T>
    {

        public int? Offset { get; set; }

        public int? Limit { get; set; }

        public int Size { get; set; }

        public Link First { get; set; }

        public Link Previous { get; set; }

        public Link Next { get; set; }

        public Link Last { get; set; }

        public T[] Value { get; set; }

    }
}
