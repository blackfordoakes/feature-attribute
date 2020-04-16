using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace feature_attribute_api.Models
{
    public class TokenSettings
    {
        /// <summary>
        /// Gets or sets the secret key.
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets the valid days.
        /// </summary>
        public int ValidDays { get; set; }
    }
}
