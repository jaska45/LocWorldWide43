import { Component, Input, OnInit } from '@angular/core';
import { Sport } from './sport';

@Component({
  selector: 'app-sport',
  templateUrl: './sport.component.html',
  styleUrls: ['./sport.component.css']
})
export class SportComponent implements OnInit 
{
  @Input()
  value: Sport;

  constructor()
  {
  }

  ngOnInit(): void {
  }

}
