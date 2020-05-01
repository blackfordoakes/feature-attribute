# feature-attribute

Shows you how to lock down your API resources by enabled features

## Overview

When building an API, we quite often do authentication and stop at that, but what about authorization? How do you know if the user has access to perform the certain operation or not? Well, it's pretty simple in .NET Core. This demo API will use JWT to authenticate, then use attributes to determine whether or not the user has access to that feature.

## Code walkthrough

I'm not going to go into detail setting up authentication. It's assumed that you already know how to do this, but you can check out `Startup.cs` and `JWTAuthenticationManager.cs` to see how it's done.

In JWTAuthenticationManager, we are looking for two API Keys:  standard or deluxe. Depending on which api key you send, we set an array of available features in our claims.

``` c#
    List<Claim> claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, apiKey),
        new Claim(ClaimTypes.NameIdentifier, apiKey),
        new Claim("features", JsonConvert.SerializeObject(availableFeatures)),
        new Claim("version", "1.0")
    };
```

We use the claims from the principal in our `FeatureFlag` attribute created in our `FeatureFlagAttribute.cs` class. Inside this class, it grabs the feature array from the claims and checks to see if the feature required for the operation is in our allowed features. If it isn't, we return a 403 Forbidden HTTP Status Code.

Then in `WeatherForecastController.cs`, we tag our actions with the feature attribute like so:

``` c#
    [FeatureFlag(Models.Features.GetForecast)]
    [HttpGet("forecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        ...
    }

    [FeatureFlag(Models.Features.MakeItRain)]
    [HttpGet("rain")]
    public IEnumerable<WeatherForecast> MakeItRain()
    {
        ...
    }
```

If the API key provided has the `GetForecast` feature, it is allowed to use the first method. If the key has the `MakeItRain` feature, it is allowed to use the second.

## How to run the project(s)

Open the solution and hit F5.

## Testing

Included in this repo is a [Postman](https://www.getpostman.com/) collection with a sample input. You can see that when you authenticate with the "standard" API Key, you can access the forecast resource, but not the makeItRain resource. When you authenticate with the "deluxe" API Key, you can access both features.

## Contributors

Just me, Blackford Oakes.