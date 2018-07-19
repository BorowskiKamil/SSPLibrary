using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace SSPLibrary.Models
{
    public class Link
    {
        public const string GetMethod = "GET";

        [JsonProperty(Order = -4)]
        public string Href { get; set; }

        [JsonProperty(Order = -3, NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(GetMethod)]
        public string Method { get; set; }

        [JsonProperty(Order = -2, PropertyName = "rel", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Relations { get; set; }

        [JsonIgnore]
        public string RouteName { get; set; }

        [JsonIgnore]
        public object RouteValues { get; set; }

        public static Link ToCollection(string routeName, object routeValues = null)
            => new Link
            {
                RouteName = routeName,
                RouteValues = routeValues,
                Method = GetMethod,
                Relations = new string[] { "collection" }
            };

    }
}
