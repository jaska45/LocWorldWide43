# Multilingual ASP.NET Core Application

This project uses ASP.NET Core 5.0 Razor Pages. The application is multilingual, so it servers each user in her language if possible.  The application uses the [standard ASP.NET request localization](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization) where the locale detection has been modified a bit. The standard order is URL parameter, cookie, browser locale. Here we have an option to replace the browser locale with geolocation. The configuration file, `appsettings.json`, can have two properties: `UseGeolocation` and `IpStackKey`. To turn browser language off and geolocation on, set `UseGeolocation` to true and set `IpStackKey` to contains your personal ipstack API access key. You can get a free key from [ipstack webpage](https://ipstack.com/).

```json
{
  "UseGeolocation": true,
  "IpStackKey": "<your_ipstack_key>"
}
```

During the middleware configuration, the application registers URL and cookie culture providers and then either an IP culture provider or an Accept-Language culture provider.

```c#
  options.RequestCultureProviders.Clear();
  options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider());
  options.RequestCultureProviders.Add(new CookieRequestCultureProvider());

  if (SportModel.UseGeolocation && !string.IsNullOrEmpty(Locator.Key))
  {
    SportModel.IpRequestCultureProvider = new IpRequestCultureProvider();
    options.RequestCultureProviders.Add(SportModel.IpRequestCultureProvider);
  }
  else
  {
    options.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
  }
```
`IpRequestCultureProvider` is a custom culture provider implemented in `IpRequestCultureProvider.cs` file. The class uses ipstack API to perform the geolocation and caches the results, so the application does not need to locate the same IP more than once.

.NET does not have support for plurals. This is why the application uses [Soluling's .NET API](https://github.com/soluling/I18N) to handle plural enabled message strings. Soluling API is a simple .NET Standard API that implements [ICU](http://site.icu-project.org/) message format and includes corresponding [CLDR](http://cldr.unicode.org/) data. 

Learn more about ASP.NET Core localization from [here](https://www.soluling.com/Help/ASP.NETCore/Index.htm).

