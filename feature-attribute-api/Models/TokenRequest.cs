using System;
using System.ComponentModel.DataAnnotations;

namespace feature_attribute_api.Models
{
    /// <summary>
    /// Information required to generate a JWT to make authorized requests to Places API.
    /// </summary>
    public class TokenRequest
    {
        /// <summary>
        /// The API Key.
        /// </summary>
        /// <example>ABCDEFGHIJKLMNOPQRSTUVWXYZ123456</example>
        [Required]
        public string ApiKey { get; set; }
    }
}
