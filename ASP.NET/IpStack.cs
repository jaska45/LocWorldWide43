using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SportApp
{
  public class IpLanguage
  {
     public string Code { get; set; }
     public string Name { get; set; }
     public string Native { get; set; }
  }

  public class IpLocationData
  {
     [JsonProperty("geoname_id")]
     public string GeonameId { get; set; }

     public string Capital { get; set; }

     public IpLanguage[] Languages { get; set; }

     [JsonProperty("country_flag")]
     public string CountryFlag { get; set; }

     [JsonProperty("country_flag_emoji")]
     public string CountryFlagEmoji { get; set; }

     [JsonProperty("country_flag_emoji_unicode")]
     public string CountryFlagEmojiUnicode { get; set; }

     [JsonProperty("calling_code")]
     public string CallingCode { get; set; }

     [JsonProperty("is_eu")]
     public bool IsEU { get; set; }
  }

  public class IpLocation
  {
     public string Ip { get; set; }
     public string HostName { get; set; }
     public string Type { get; set; }

     [JsonProperty("continent_code")]
     public string ContinentCode { get; set; }

     [JsonProperty("continent_name")]
     public string ContinentName { get; set; }

     [JsonProperty("country_code")]
     public string CountryCode { get; set; }

     [JsonProperty("country_name")]
     public string CountryName { get; set; }

     [JsonProperty("region_code")]
     public string RegionCode { get; set; }

     [JsonProperty("region_name")]
     public string RegionName { get; set; }

     public string City { get; set; }
     public string Zip { get; set; }
     public double Latitude { get; set; }
     public double Longitude { get; set; }

     public IpLocationData Location { get; set; }
  }

  public class Locator
  {
    static public string Key { get; set; }

    public Locator()
    {
    }

    public async Task<IpLocation> Locate(string ipAddress)
    {
      using var client = new HttpClient();
      try
      {
        var url = $"http://api.ipstack.com/{ipAddress}?access_key={Key}";
        var response = await client.GetAsync(url);

        if (response.StatusCode == HttpStatusCode.OK)
        {
          var json = response.Content.ReadAsStringAsync().Result;
          return JsonConvert.DeserializeObject<IpLocation>(json);
        }
        else
          return null;
      }
      catch (Exception)
      {
        return null;
      }
    }
  }
}
