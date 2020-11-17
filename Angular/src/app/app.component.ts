import { Component, OnInit } from '@angular/core';
import { Sport } from './sport/sport';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent  implements OnInit
{
  sports: Sport[] = [];

  ngOnInit()
  {
    this.sports.push(new Sport(
      $localize`Soccer`, 
      $localize`Soccer is a sport played between two teams of eleven players with a spherical ball.`, 
      $localize`England`, 
      10, 
      'soccer_ball.png')); 

    this.sports.push(new Sport(
      $localize`Ice hockey`, 
      $localize`Ice hockey is a team sport played on ice, in which skaters use sticks to direct a puck into the opposing team's goal.`, 
      $localize`Canada`, 
      5, 
      'hockey_stick.png')); 

    this.sports.push(new Sport(
      $localize`Basketball`, 
      $localize`Basketball is a team sport in which two teams of five players try to score points by throwing a ball through the top of a basketball hoop while following a set of rules.`, 
      $localize`United States`, 
      5, 
      'basketball.png')); 

    this.sports.push(new Sport(
      $localize`Alpine skiing`, 
      $localize`Alpine skiing is the sport or recreation of sliding down snow-covered hills on skis with fixed-heel bindings.`, 
      $localize`Norway`, 
      1, 
      'cloud_snow.png')); 
  }
}
