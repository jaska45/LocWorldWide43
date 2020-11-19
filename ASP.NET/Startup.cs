using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Soluling.AspNet;
using SportApp.Pages;

namespace SportApp
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddRazorPages()
        .AddViewLocalization()
        .AddDataAnnotationsLocalization();

      services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      Locator.Key = Configuration["IpStackKey"];
      SportModel.UseGeolocation = Configuration.GetValue<bool>("UseGeolocation") && (Locator.Key != "");
      SportModel.SupportedCultures = BuilderExtension.GetSupportedLanguages(Assembly.GetExecutingAssembly().Location, "en");

      var options = new RequestLocalizationOptions
      {
        DefaultRequestCulture = new RequestCulture("en"),
        SupportedCultures = SportModel.SupportedCultures,
        SupportedUICultures = SportModel.SupportedCultures
      };

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

      app.UseRequestLocalization(options);

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapRazorPages();
      });
    }
  }
}
