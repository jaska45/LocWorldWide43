# Multilingual Angular Application

This project uses Angular 11. The project uses standard Angular internationalization and localization. However, it does not follow the standard Angular way where you compile and deploy each language separately.  Instead, the application loads translations on runtime. The loading happens in `main.ts` before the application bootstraps itself. 

Let's go through what happens in `main.ts`. Before bootstrapping the application the application calls `getDefaultLanguage` that tries to detect the active locale/language. If the user has previously selected the language, that language is used. The user interface contains a combo box where the user can select the language. By default, there is no selected language. In that case, the application tries to detect the language.

If there is no stored language, then the application tries to detect the language(s). By default, the application uses the locales of the browser, but you can also configure it to geolocating the user's IP address. If enabled, the application uses [ipstack](https://ipstack.com/) API to geolocate the IP address. If you want to use this feature, you have to set the two parameters in the `environemtn.ts` file. First, set the `useGeolocation` parameter true and set the `ipStackKey` parameter to contains your ipstack API access key. You can get a free key from [ipstack webpage](https://ipstack.com/).

```json
export const environment = {
 useGeolocation: true,
 ipStackKey: "<your_ipstack_key>"
};
```

Now the application might have the locale or might not have the locale. In either way, it calls `getTranslationsEx` from [Soluling's Angular library](https://github.com/soluling/I18N/tree/master/Library/Angular) to load the translations. If no locale was passed, the function gets the locale(s) from the browser. At this point, the library knows what locale(s) to use. `getTranslationsEx`  tries to find a resource file matching the locale or one of the locale if many locales were detected. If a matching resource was found, `getTranslationsEx`  reads it and returns the translations of the resource file. The application then passes the translations to Angular runtime by calling `loadTranslations` (this is part of Angular), turning the application to use the translations just loaded. Finally, the application gets bootstrapped. 

Shorty, the application select the language using this rule:

1. Use whatever language stored in the local storage, if any
2. Use the browser's language or geolocate the user's IP and pick a language based on the location.

The rest of the application follows Angular's [internationalization guideline](https://angular.io/guide/i18n) including Angular's built-in support for plurals. Internally Angular uses [ICU](http://site.icu-project.org/) message format and includes corresponding [CLDR](http://cldr.unicode.org/) data. You can read more about Angular localization from [here](https://www.soluling.com/Help/Angular/Index.htm).

