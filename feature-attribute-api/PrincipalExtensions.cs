using System;
using System.Collections.Generic;
using System.Security.Claims;
using feature_attribute_api.Models;
using Newtonsoft.Json;

namespace feature_attribute_api
{
    /// <summary>
    /// Extensions for the ClaimsPrincipal class.
    /// </summary>
    public static class PrincipalExtensions
    {
        /// <summary>
        /// Retrieves the value of AccountId from any claim.
        /// </summary>
        /// <param name="claimsPrincipal">The current principal.</param>
        /// <returns></returns>
        public static List<Features> GetFeatures(this ClaimsPrincipal claimsPrincipal)
        {
            List<Features> availableFeatures = new List<Features>();

            Claim claim = claimsPrincipal.FindFirst(c => c.Type == "features");
            if (claim != null)
            {
                availableFeatures = JsonConvert.DeserializeObject<List<Features>>(claim.Value);
            }

            return availableFeatures;
        }
    }
}
