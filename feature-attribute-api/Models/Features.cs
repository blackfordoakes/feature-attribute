using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace feature_attribute_api.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Features
    {
        GetForecast = 1,
        MakeItRain = 2
    }
}
