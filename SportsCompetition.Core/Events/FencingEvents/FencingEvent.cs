using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Scores;
using SportsCompetition.Core.States;

namespace SportsCompetition.Core.Events;

public abstract class FencingEvent : IEvent<FencingScore, Player>
{   
    public abstract void Apply(Dictionary<Player, FencingScore> scores);
}