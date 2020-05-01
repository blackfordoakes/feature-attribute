using System;
using System.Linq;
using feature_attribute_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace feature_attribute_api
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class FeatureFlagAttribute : ActionFilterAttribute
    {
        private readonly Features _featureName;

        public FeatureFlagAttribute(Features featureName)
        {
            _featureName = featureName;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // get the features from the JWT
            var features = context.HttpContext.User.GetFeatures();
            var found = features.Any(f => f == _featureName);

            // if the required feature is not in the authorized feature, return a forbidden
            if (!found)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
