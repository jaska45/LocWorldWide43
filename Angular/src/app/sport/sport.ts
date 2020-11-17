// A sport class
export class Sport
{
  name: string;
  description: string;
  origin: string;
  players: number;
  image: string;

  constructor(name: string, description: string, origin: string, players: number, image: string)
  {
    this.name = name;
    this.description = description;
    this.origin = origin;
    this.players = players;
    this.image = image;
  }
}