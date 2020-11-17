import { Component, OnInit } from '@angular/core';

export class Language
{
  id: string;
  name: string;
  native: string;

  constructor(id: string, name: string, native: string)
  {
    this.id = id;
    this.name = name;
    this.native = native;
  }
}

@Component({
  selector: 'app-select-language',
  templateUrl: './select-language.component.html',
  styleUrls: ['./select-language.component.css']
})
export class SelectLanguageComponent implements OnInit 
{
  languages: Language[] = [];
  selected: string;

  constructor() { }

  ngOnInit(): void 
  {
    this.selected = localStorage.getItem('language');

    this.languages.push(new Language('', $localize`Default`, $localize`Detected language`));
    this.languages.push(new Language('en', $localize`English`, 'English'));
    this.languages.push(new Language('fi', $localize`Finnish`, 'suomi'));
    this.languages.push(new Language('de', $localize`German`, 'Deutsch'));
    this.languages.push(new Language('fr', $localize`French`, 'Français'));
    this.languages.push(new Language('ja', $localize`Japanese`, '日本語'));
  }

  onChange(value)
  {
    console.log('value', value);

    // Save the language to the local storage
    localStorage.setItem('language', value);

    // Reload
    window.location.reload();
  }
}
