import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { loadTranslations } from '@angular/localize';
import { getTranslationsEx } from '@soluling/angular';

import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

// Bootstapping the application is different to the default one because we first need to get the active language
// and then load the translations for that language.
function getDefaultLanguage(key: string): Promise<string> 
{
  // If the user has already selected a language use it.
  const id = localStorage.getItem('language');

  if (id)
    return new Promise<string>((resolve) => resolve(id));

  // If geolocation is turned off return undefined
  if (!environment.useGeolocation || !key)
    return new Promise<string>((resolve) => resolve(undefined));

  // Geolocate using ipstack
  let url = `http://api.ipstack.com/check?access_key=${key}`;
  console.log('url', url);

  return fetch(url)
    .then(response => 
    {
      if (!response.ok)
        return null;

      return response.text();
    })
    .then((data: string) => 
    {
      console.log('data', data);
      let location = JSON.parse(data);
      let locale = "";

      if (location?.location?.languages)
      {
        for (let language of location.location.languages)
        {
          if (locale.length > 0)
            locale = locale + ';';
  
          locale = locale + language.code;
        }
      }
      
      return locale;
    })
    .catch(() => undefined);
}

getDefaultLanguage(environment.ipStackKey).then(id =>
  {
    // Get translations from assets/i18n folder in the server
    getTranslationsEx('assets/i18n', { locale: id }).then(translations => 
      {
        // Load translations
        if (translations)
          loadTranslations(translations);
      
        // Launch the application
        import('./app/app.module').then(module => 
        {
          platformBrowserDynamic()
            .bootstrapModule(module.AppModule)
            .catch(err => console.error(err));
        });      
      });
  }
)