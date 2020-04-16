using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace feature_attribute_api.Models
{
    /// <summary>
    /// The response from an authorization request.
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// The JWT used to access Places API.
        /// </summary>
        /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c</example>
        [DataMember(EmitDefaultValue = false)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        /// <summary>
        /// The date and time this token expires.
        /// </summary>
        /// <example>2020-04-42T10:13:29.9007619+00:00</example>
        [DataMember(EmitDefaultValue = false)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Expires { get; set; }
    }
}
