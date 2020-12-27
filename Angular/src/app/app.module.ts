import { BrowserModule } from '@angular/platform-browser';
import { LOCALE_ID, NgModule } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import { LocaleService } from '@soluling/angular';

import { AppComponent } from './app.component';
import { SportComponent } from './sport/sport.component';
import { SelectLanguageComponent } from './select-language/select-language.component';

// Register the locales we want to support so they will be compiled to the application bundle
import de from '@angular/common/locales/de';
import fi from '@angular/common/locales/fi';
import fr from '@angular/common/locales/fr';
import ja from '@angular/common/locales/ja';

registerLocaleData(de, 'de');
registerLocaleData(fi, 'fi');
registerLocaleData(fr, 'fr');
registerLocaleData(ja, 'ja');

@NgModule({
  declarations: [
    AppComponent,
    SportComponent,
    SelectLanguageComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: 
  [
    // This service tells us the active locale
    LocaleService, 

    // Use the locale service to get an active value to LOCALE_ID
    { provide: LOCALE_ID, deps: [LocaleService], useFactory: (service: LocaleService) => service.localeId },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
