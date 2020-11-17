using System.Collections.ObjectModel;
using Microsoft.Extensions.Localization;
using Soluling;

namespace SportApp.Pages
{
  public class IndexModel : SportModel
  {
    private readonly IStringLocalizer<IndexModel> localizer;

    public ReadOnlyCollection<Sport> Values;

    public IndexModel(IStringLocalizer<IndexModel> localizer)
    {
      this.localizer = localizer;
    }

    public string GetPlayers(Sport sport)
    {
      return MultiPattern.FormatMulti(localizer["{plural, one {{0} player} other {{0} players}}"], sport.Players); //loc 0: Number of players
    }

    public void OnGet()
    {
      Values = new ReadOnlyCollection<Sport>(
        new[]
        {
          new Sport 
          { 
            Name = localizer["Soccer"], 
            Description = localizer["Soccer is a sport played between two teams of eleven players with a spherical ball."], 
            Origin = localizer["England"], 
            Players = 10, 
            Image = "soccer_ball.png" 
          },

          new Sport 
          { 
            Name = localizer["Ice hockey"], 
            Description = localizer["Ice hockey is a team sport played on ice, in which skaters use sticks to direct a puck into the opposing team's goal."], 
            Origin = localizer["Canada"], 
            Players = 5, 
            Image = "hockey_stick.png" 
          },

          new Sport 
          { 
            Name = localizer["Basketball"], 
            Description = localizer["Basketball is a team sport in which two teams of five players try to score points by throwing a ball through the top of a basketball hoop while following a set of rules."], 
            Origin = localizer["United States"], 
            Players = 5, 
            Image = "basketball.png" 
          },

          new Sport 
          { 
            Name = localizer["Alpine skiing"], 
            Description = localizer["Alpine skiing is the sport or recreation of sliding down snow-covered hills on skis with fixed-heel bindings."], 
            Origin = localizer["Norway"], 
            Players = 1, 
            Image = "cloud_snow.png" 
          }
        });
    }
  }
}
