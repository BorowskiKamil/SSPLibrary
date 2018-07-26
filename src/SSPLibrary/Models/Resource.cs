using System;
using Newtonsoft.Json;

namespace SSPLibrary.Models
{
    public abstract class Resource : Link
    {

        [JsonIgnore]
        public Link Self { get; set; }

    }
}
