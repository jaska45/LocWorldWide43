using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SportApp.Pages
{
  public class SportModel: PageModel
  {
    static public bool UseGeolocation { get; set; }
    static public List<CultureInfo> SupportedCultures { get; set; }
    static public IpRequestCultureProvider IpRequestCultureProvider { get; set; }

    [BindProperty]
    public Sport Value { get; set; }

    [TempData]
    public string Message { get; set; }

    private static string GetLanguagePart(string id)
    {
      return id.Split('-')[0];
    }

    public string AcceptLanguage
    {
      get
      {
        if (Request.Headers.ContainsKey("Accept-Language"))
          return GetLanguagePart(Request.Headers["Accept-Language"][0]);
        else
          return "en";
      }
    }

    public string CookieLanguage
    {
      get
      {
        var cookieValue = Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];

        if (cookieValue != null)
          return CookieRequestCultureProvider.ParseCookieValue(cookieValue).Cultures[0].Value;
        else
          return "";
      }
    }

    private string GetClientIp()
    {
      return Request.HttpContext.Connection.RemoteIpAddress.ToString();
    }

    public async Task<string> GetIpLanguage()
    {
      var location = await new Locator().Locate(GetClientIp());

      if (location != null)
      {
        return "";
      }
      else
        return "";
    }

    public string ActiveLanguage
    {
      get
      {
        if (CookieLanguage != "")
          return CookieLanguage;
        else
          return AcceptLanguage;
      }
    }

    public string GetLanguageName(string language)
    {
      language = GetLanguagePart(language);

      return language switch
      {
        "de" => LanguageNames.German,
        "en" => LanguageNames.English,
        "fi" => LanguageNames.Finnish,
        "fr" => LanguageNames.French,
        "et" => LanguageNames.Estonian,
        "ja" => LanguageNames.Japanese,
        "sv" => LanguageNames.Swedish,
        _ => new CultureInfo(language).DisplayName,
      };
    }

    public IActionResult OnPostSetLanguage(string culture)
    {
      if (!string.IsNullOrEmpty(culture))
      {
        Response.Cookies.Append(
          CookieRequestCultureProvider.DefaultCookieName,
          CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
          new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );
      }
      else
      {
        Response.Cookies.Delete(CookieRequestCultureProvider.DefaultCookieName);
      }

      return RedirectToPage();
    }
  }
}
