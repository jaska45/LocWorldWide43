using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Caching.Memory;

namespace SportApp
{
  public class IpRequestCultureProvider: RequestCultureProvider
  {
    private IMemoryCache cache = new MemoryCache(new MemoryCacheOptions
    {
      SizeLimit = 1024
    });

    private MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
      .SetSize(1)
      .SetSlidingExpiration(TimeSpan.FromHours(3));

    public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
    {
      var id = await GetId(httpContext.Connection.RemoteIpAddress.ToString());
      return new ProviderCultureResult(id);
    }

    public async Task<string> GetLanguage(string ip)
    {
      return await GetId(ip);
    }

    private async Task<string> GetId(string ip)
    {
      var result = "en";

      if (ip == "::1")
        return result;  // ip = "178.217.129.234"; // "98.35.25.50";

      if (!cache.TryGetValue(ip, out IpLocation location))
      {
        location = await new Locator().Locate(ip);

        if (location != null)
          cache.Set(ip, location, cacheEntryOptions);
      }

      if ((location != null) && (location.Location?.Languages?.Length > 0))
        result = location.Location?.Languages[0].Code;

      return result;
    }
  }
}
