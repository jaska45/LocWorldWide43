# Multilingual ASP.NET Core Application

This project uses ASP.NET Core 5.0 Razor Pages.

The application loads translations on runtime. Before loading the translations, the application detects the active locale/language. The method is this.

1. If the user has previously selected the language, that language is used. The user interface contains a combo box where the user can select the language. By default, there is no selected language. In that case, the next rule is used.
2. The application can try to detect the language(s) of the user by geolocating the user's IP address. The application uses [ipstack](https://ipstack.com/) API to geolocate the IP address. If you want to use this feature, you have to set the two parameters in the `environemtn.ts` file. First, set the `useGeolocation` parameter true and set the `ipStackKey` parameter to contains your ipstack access key. You can get a free key from [ipstack webpage](https://ipstack.com/).

Now the application might have the locale, or might not have the locale. In either way it uses [Soluling's Angular library](https://github.com/soluling/I18N/tree/master/Library/Angular) to load the translations. If no locale was passed the library gets the locale(s) from the browser. At this point the library knows what locale(s) to use. The library tries to find a resource file matching the locale or one of the locale if many locales were detected. If a matching resource was found the library reads it and returns the translations of the resource file. The application then passes the translations to Angular runtime turning the application to use the translations. 

All this is done in `main.ts` file before bootstrapping the application. The rest of the application follows Angular's [internationalization guideline](https://angular.io/guide/i18n). You can read more about Angular localization from [here](https://www.soluling.com/Help/Angular/Index.htm).