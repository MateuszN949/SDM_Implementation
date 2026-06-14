using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Scores;
using SportsCompetition.Core.States;

namespace SportsCompetition.Core.Events;

public abstract class FootballEvent : IEvent<FootballScore, Team>
{   
    public abstract void Apply(Dictionary<Team, FootballScore> scores);
}